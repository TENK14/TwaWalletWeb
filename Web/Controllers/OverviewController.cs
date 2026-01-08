using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwaWallet.Model;
using TwaWallet.Web.DataLayer;
using TwaWallet.Web.Models.OverviewViewModels;

namespace TwaWallet.Web.Controllers
{
    [Authorize]
    public class OverviewController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDataLayer dataLayer;

        public OverviewController(
            UserManager<ApplicationUser> userManager,
            IDataLayer dataLayer)
        {
            this.userManager = userManager;
            this.dataLayer = dataLayer;
        }

        private ApplicationUser ApplicationUser
        {
            get
            {
                return userManager.GetUserAsync(User).Result;
            }
        }

        public async Task<IActionResult> Index(string periodType = "YTD", int? year = null, int? month = null, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var today = DateTime.Now.Date;
            DateTime calculatedDateFrom;
            DateTime calculatedDateTo;

            switch (periodType)
            {
                case "YTD":
                    calculatedDateFrom = new DateTime(today.Year, 1, 1);
                    calculatedDateTo = today;
                    ViewBag.PeriodDescription = $"Year to Date ({today.Year})";
                    break;

                case "Year":
                    var selectedYear = year ?? today.Year;
                    calculatedDateFrom = new DateTime(selectedYear, 1, 1);
                    calculatedDateTo = new DateTime(selectedYear, 12, 31);
                    ViewBag.PeriodDescription = $"Year {selectedYear}";
                    break;

                case "Month":
                    var selectedYear2 = year ?? today.Year;
                    var selectedMonth = month ?? today.Month;
                    calculatedDateFrom = new DateTime(selectedYear2, selectedMonth, 1);
                    calculatedDateTo = new DateTime(selectedYear2, selectedMonth, DateTime.DaysInMonth(selectedYear2, selectedMonth));
                    ViewBag.PeriodDescription = $"{selectedMonth}/{selectedYear2}";
                    break;

                case "Custom":
                    if (dateFrom.HasValue && dateTo.HasValue)
                    {
                        calculatedDateFrom = dateFrom.Value.Date;
                        calculatedDateTo = dateTo.Value.Date;
                        ViewBag.PeriodDescription = $"{calculatedDateFrom:dd.MM.yyyy} - {calculatedDateTo:dd.MM.yyyy}";
                    }
                    else
                    {
                        calculatedDateFrom = new DateTime(today.Year, today.Month, 1);
                        calculatedDateTo = today;
                        ViewBag.PeriodDescription = "Custom period";
                    }
                    break;

                default:
                    calculatedDateFrom = new DateTime(today.Year, 1, 1);
                    calculatedDateTo = today;
                    ViewBag.PeriodDescription = $"Year to Date ({today.Year})";
                    periodType = "YTD";
                    break;
            }

            ViewBag.DateFrom = calculatedDateFrom;
            ViewBag.DateTo = calculatedDateTo;
            ViewBag.PeriodType = periodType;
            ViewBag.Year = year ?? today.Year;
            ViewBag.Month = month ?? today.Month;

            var records = dataLayer.GetUserRecords(ApplicationUser);

            records = records.Where(r => r.Date >= calculatedDateFrom && r.Date <= calculatedDateTo);

            var list = await records.ToListAsync();

            var categories = dataLayer.GetUserCategories(ApplicationUser).ToList();

            var groupedByCategory = list.GroupBy(r => r.CategoryId);

            var result = new List<OverviewVM>();

            foreach (var group in groupedByCategory)
            {
                var category = categories.FirstOrDefault(c => c.Id == group.Key);
                var expenses = group.Where(r => !r.Earnings).Sum(r => Math.Abs(r.Cost));
                var earnings = group.Where(r => r.Earnings).Sum(r => r.Cost);

                result.Add(new OverviewVM
                {
                    CategoryDescription = category?.Description ?? "Unknown",
                    Expenses = expenses,
                    Earnings = earnings,
                    Total = earnings - expenses
                });
            }

            result = result.OrderByDescending(r => Math.Abs(r.Expenses)).ToList();

            return View(result);
        }
    }
}

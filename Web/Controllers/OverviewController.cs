using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        // TODO: periodType zrušit
        public async Task<IActionResult> Index(string periodType = "YTD", string aggregationType = "Month", int? year = null, int? month = null, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var today = DateTime.Now.Date;
            DateTime calculatedDateFrom;
            DateTime calculatedDateTo;

            calculatedDateFrom = dateFrom.HasValue ? dateFrom.Value.Date : new DateTime(today.Year, 1, 1);
            calculatedDateTo = dateTo.HasValue ? dateTo.Value.Date : today;

            //switch (periodType)
            //{
            //    case "YTD":
            //        calculatedDateFrom = new DateTime(today.Year, 1, 1);
            //        calculatedDateTo = today;
            //        ViewBag.PeriodDescription = $"Year to Date ({today.Year})";
            //        break;

            //    case "Year":
            //        var selectedYear = year ?? today.Year;
            //        calculatedDateFrom = new DateTime(selectedYear, 1, 1);
            //        calculatedDateTo = new DateTime(selectedYear, 12, 31);
            //        ViewBag.PeriodDescription = $"Year {selectedYear}";
            //        break;

            //    case "Month":
            //        var selectedYear2 = year ?? today.Year;
            //        var selectedMonth = month ?? today.Month;
            //        calculatedDateFrom = new DateTime(selectedYear2, selectedMonth, 1);
            //        calculatedDateTo = new DateTime(selectedYear2, selectedMonth, DateTime.DaysInMonth(selectedYear2, selectedMonth));
            //        ViewBag.PeriodDescription = $"{selectedMonth}/{selectedYear2}";
            //        break;

            //    case "Custom":
            //        if (dateFrom.HasValue && dateTo.HasValue)
            //        {
            //            calculatedDateFrom = dateFrom.Value.Date;
            //            calculatedDateTo = dateTo.Value.Date;
            //            ViewBag.PeriodDescription = $"{calculatedDateFrom:dd.MM.yyyy} - {calculatedDateTo:dd.MM.yyyy}";
            //        }
            //        else
            //        {
            //            calculatedDateFrom = new DateTime(today.Year, today.Month, 1);
            //            calculatedDateTo = today;
            //            ViewBag.PeriodDescription = "Custom period";
            //        }
            //        break;

            //    default:
            //        calculatedDateFrom = new DateTime(today.Year, 1, 1);
            //        calculatedDateTo = today;
            //        ViewBag.PeriodDescription = $"Year to Date ({today.Year})";
            //        periodType = "YTD";
            //        break;
            //}

            ViewBag.DateFrom = calculatedDateFrom;
            ViewBag.DateTo = calculatedDateTo;
            //ViewBag.PeriodType = periodType;
            ViewBag.AggregationType = aggregationType;
            ViewBag.Year = year ?? today.Year;
            ViewBag.Month = month ?? today.Month;

            var records = dataLayer.GetUserRecords(ApplicationUser);

            records = records.Where(r => r.Date >= calculatedDateFrom && r.Date <= calculatedDateTo);

            var list = await records.ToListAsync();

            var result = new List<OverviewVM>();

            if (aggregationType == "Month")
            {
                var groupedByMonth = list.GroupBy(r => new { r.Date.Year, r.Date.Month });

                foreach (var group in groupedByMonth.OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month))
                {
                    var expenses = group.Where(r => !r.Earnings).Sum(r => Math.Abs(r.Cost));
                    var earnings = group.Where(r => r.Earnings).Sum(r => r.Cost);

                    result.Add(new OverviewVM
                    {
                        Period = new DateTime(group.Key.Year, group.Key.Month, 1).ToString("yyyy/MM", CultureInfo.InvariantCulture),
                        Expenses = expenses,
                        Earnings = earnings,
                        Total = earnings - expenses
                    });
                }
            }
            else if (aggregationType == "Year")
            {
                var groupedByYear = list.GroupBy(r => r.Date.Year);

                foreach (var group in groupedByYear.OrderBy(g => g.Key))
                {
                    var expenses = group.Where(r => !r.Earnings).Sum(r => Math.Abs(r.Cost));
                    var earnings = group.Where(r => r.Earnings).Sum(r => r.Cost);

                    result.Add(new OverviewVM
                    {
                        Period = group.Key.ToString(),
                        Expenses = expenses,
                        Earnings = earnings,
                        Total = earnings - expenses
                    });
                }
            }

            return View(result);
        }
    }
}

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
using TwaWallet.Web.Models.SummaryViewModels;

namespace TwaWallet.Web.Controllers
{
    public class SummaryController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDataLayer dataLayer;

        public SummaryController(
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


        public async Task<IActionResult> Index(DateTime? dateFrom, DateTime? dateTo, bool earnings = false)
        {
            var today = DateTime.Now.Date;

            if (!dateFrom.HasValue)
            {
                var past = today.AddMonths(-12);

                dateFrom = new DateTime(past.Year, past.Month, 1);
            }

            if (!dateTo.HasValue)
            {
                dateTo = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
            }

            ViewBag.DateFrom = dateFrom.HasValue ? dateFrom.Value.Date : (DateTime?)null;
            ViewBag.DateTo = dateTo.HasValue ? dateTo.Value.Date : (DateTime?)null;
            ViewBag.Earnings = earnings;

            var records = dataLayer.GetUserRecords(ApplicationUser);

            if (dateFrom != null)
            {
                records = records.Where(r => r.Date >= dateFrom);
            }

            if (dateTo != null)
            {
                records = records.Where(r => r.Date <= dateTo);
            }

            var list = await records.ToListAsync();

            if (!ViewBag.Earnings)
            {
                list = list.Where(r => !r.Earnings).ToList();
            }

            foreach (var item in list)
            {
                if (!item.Earnings)
                {
                    item.Cost = 0 - item.Cost;
                }
            }

            IEnumerable<IGrouping<string, Record>> groups = list.GroupBy(i => i.Date.ToString("yyyy/MM", CultureInfo.InvariantCulture));


            var result = new List<SummaryVM>();


            foreach (var item in groups)
            {
                result.Add(new SummaryVM
                {
                    Period = item.Key,
                    Expenses = item.Where(i => !i.Earnings).Sum(i => i.Cost),
                    Earnings = item.Where(i => i.Earnings).Sum(i => i.Cost)
                });
            }


            ViewBag.List = result;

            return View(result);
        }

        
    }
}

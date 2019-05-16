using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TwaWallet.Entity;
using TwaWallet.Model;
using TwaWallet.Web.DataLayer;
using TwaWallet.Web.Models.Record;

namespace TwaWallet.Web.Controllers
{
    [Authorize]
    //[Route("[controller]/[action]")]
    public class RecordsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDataLayer dataLayer;

        public int MyProperty { get; set; }

        private ApplicationUser ApplicationUser
        {
            get
            {
                return userManager.GetUserAsync(User).Result;
            }
        }

        public RecordsController(
            UserManager<ApplicationUser> userManager,
            IDataLayer dataLayer)
        {
            this.userManager = userManager;
            this.dataLayer = dataLayer;

            //if (User != null)
            //{
            //    this.user = userManager.GetUserAsync(User).Result;
            //}
        }

        //// GET: Records
        //public async Task<IActionResult> Index()
        //{
        //    //var applicationDbContext = GetUsersRecords();
        //    var records = _dataLayer.GetUserRecords();

        //    if (records == null
        //        || !records.Any())
        //    {
        //        //return NotFound();
        //    }

        //    _dataLayer.Load(records, r => r.Category);
        //    _dataLayer.Load(records, r => r.PaymentType);

        //    return View(await records.ToListAsync());

        //}

        //GET: Records
        public async Task<IActionResult> Index(DateTime? dateFrom, DateTime? dateTo, string searchString, bool earnings = false)
        {
            //ViewData["DateFrom"] = dateFrom;
            //ViewData["DateTo"] = dateTo;
            //ViewData["CurrentFilter"] = searchString;

            if (!dateFrom.HasValue)
            {
                var today = DateTime.Now.Date;
                dateFrom = new DateTime(today.Year, today.Month, 1);
            }

            if (!dateTo.HasValue)
            {
                var today = DateTime.Now.Date;
                dateTo = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
            }

            ViewBag.DateFrom = dateFrom.HasValue ? dateFrom.Value.Date : (DateTime?)null;
            ViewBag.DateTo = dateTo.HasValue? dateTo.Value.Date : (DateTime?)null;
            ViewBag.CurrentFilter = searchString;
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

            dataLayer.Load(records, r => r.Category);
            dataLayer.Load(records, r => r.PaymentType);

            if (!String.IsNullOrWhiteSpace(searchString))
            {
                records = records.Where(r => r.Description.Contains(searchString)
                || r.Tag.Contains(searchString)
                || r.Category.Description.Contains(searchString)
                || r.PaymentType.Description.Contains(searchString));
            }

            if (records == null
                || !records.Any())
            {
                //return NotFound();
            }

            var list = await records.ToListAsync();
            //var list = await records.Select(r => { (r.Earnings == true ? r.Cost = r.Cost : r.Cost = 0 - r.Cost); return r; }).ToListAsync();

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

            ViewBag.List = list;

            return View(list);

        }


        // Partial Views
        //public async Task<IActionResult> Index2([Bind("DateFrom,DateTo,CurrentFilter")]FilterIM filterIM)
        public async Task<IActionResult> Index2([Bind("DateFrom,DateTo,SearchString")]FilterIM filterIM)


        {
            ViewBag.Filter = filterIM;
            //ViewData["DateFrom"] = filterIM.DateFrom;
            //ViewData["DateTo"] = filterIM.DateTo;
            //ViewData["CurrentFilter"] = filterIM.SearchString;

            ViewBag.DateFrom = filterIM.DateFrom;
            ViewBag.DateTo = filterIM.DateTo;
            ViewBag.CurrentFilter = filterIM.SearchString;

            var records = dataLayer.GetUserRecords(ApplicationUser);

            if (filterIM.DateFrom != null)
            {
                records = records.Where(r => r.Date >= filterIM.DateFrom);
            }

            if (filterIM.DateTo != null)
            {
                records = records.Where(r => r.Date <= filterIM.DateTo);
            }

            dataLayer.Load(records, r => r.Category);
            dataLayer.Load(records, r => r.PaymentType);

            if (!String.IsNullOrWhiteSpace(filterIM.SearchString))
            {
                records = records.Where(r => r.Description.Contains(filterIM.SearchString)
                || r.Tag.Contains(filterIM.SearchString)
                || r.Category.Description.Contains(filterIM.SearchString)
                || r.PaymentType.Description.Contains(filterIM.SearchString));
            }

            if (records == null
                || !records.Any())
            {
                //return NotFound();
            }

            var list = await records.ToListAsync();
            ViewBag.List = list;

            return View(list);

        }

        // GET: Records/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = dataLayer.GetUserRecord(ApplicationUser, id);

            if (record == null)
            {
                return NotFound();
            }

            dataLayer.Load(new List<Record> { record }, r => r.Category);
            dataLayer.Load(new List<Record> { record }, r => r.PaymentType);

            return View(record);
        }

        public IActionResult SetCreate(Record record)
        {
            ViewBag.CategoryId = new SelectList(dataLayer.GetUserCategories(ApplicationUser).OrderByDescending(c => c.IsDefault), $"{nameof(Category.Id)}", $"{nameof(Category.Description)}");
            // alternativa
            //ViewData["CategoryId"] = new SelectList(_dataLayer.GetUserCategories().OrderByDescending(c => c.IsDefault), $"{nameof(Category.Id)}", $"{nameof(Category.Description)}");
            ViewBag.PaymentTypeId = new SelectList(dataLayer.GetUserPaymentTypes(ApplicationUser).OrderByDescending(p => p.IsDefault), $"{nameof(PaymentType.Id)}", $"{nameof(PaymentType.Description)}");

            ViewBag.Date = DateTime.Now;

            return View(record);
        }

        // GET: Records/Create
        public IActionResult Create()
        {
            var record = new Record();
            return SetCreate(record);

            //ViewBag.CategoryId = new SelectList(dataLayer.GetUserCategories(ApplicationUser).OrderByDescending(c => c.IsDefault), $"{nameof(Category.Id)}", $"{nameof(Category.Description)}");
            //// alternativa
            ////ViewData["CategoryId"] = new SelectList(_dataLayer.GetUserCategories().OrderByDescending(c => c.IsDefault), $"{nameof(Category.Id)}", $"{nameof(Category.Description)}");
            //ViewBag.PaymentTypeId = new SelectList(dataLayer.GetUserPaymentTypes(ApplicationUser).OrderByDescending(p => p.IsDefault), $"{nameof(PaymentType.Id)}", $"{nameof(PaymentType.Description)}");

            //ViewBag.Date = DateTime.Now;

            //return View(record);
        }

        // GET: Records/Create
        //public IActionResult Create2()
        //{
        //    var record = new Record();

        //    ViewBag.CategoryId = new SelectList(_dataLayer.GetUserCategories().OrderByDescending(c => c.IsDefault), $"{nameof(Category.Id)}", $"{nameof(Category.Description)}");
        //    ViewBag.PaymentTypeId = new SelectList(_dataLayer.GetUserPaymentTypes().OrderByDescending(p => p.IsDefault), $"{nameof(PaymentType.Id)}", $"{nameof(PaymentType.Description)}");

        //    ViewBag.Date = DateTime.Now;

        //    return View(record);
        //}

        // POST: Records/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cost,Description,CategoryId,Date,Warranty,ApplicationUserId,PaymentTypeId,Tag,Earnings,Id")] Record recordIM)
        {
            if (recordIM == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid
                || (ModelState.ErrorCount == 1
                    && ModelState.GetValidationState(nameof(Record.ApplicationUserId)) == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    )
            {
                var user = await userManager.GetUserAsync(User);

                recordIM.ApplicationUser = user;

                dataLayer.AddAsync(recordIM);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", $"{nameof(ApplicationUser.UserName)}");
            
            ViewData["CategoryId"] = new SelectList(dataLayer.GetUserCategories(ApplicationUser), "Id", $"{nameof(Category.Description)}", recordIM.Category);
            ViewData["PaymentTypeId"] = new SelectList(dataLayer.GetUserPaymentTypes(ApplicationUser), "Id", $"{nameof(PaymentType.Description)}");
            ViewBag.Date = DateTime.Now;
            ViewBag.Warranty = 0;

            return View(recordIM);
        }

        // GET: Records/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = dataLayer.GetUserRecord(ApplicationUser, id);
            if (record == null)
            {
                return NotFound();
            }

            dataLayer.Load(new List<Record> { record }, r => r.Category);
            dataLayer.Load(new List<Record> { record }, r => r.PaymentType);

            //ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", record.ApplicationUserId);
            ViewData["CategoryId"] = new SelectList(dataLayer.GetUserCategories(ApplicationUser), "Id", $"{nameof(Category.Description)}", record.CategoryId);
            ViewData["PaymentTypeId"] = new SelectList(dataLayer.GetUserPaymentTypes(ApplicationUser), "Id", $"{nameof(PaymentType.Description)}", record.PaymentTypeId);
            ViewData["Date"] = record.Date;
            return View(record);
        }

        // POST: Records/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Cost,Description,CategoryId,Date,Warranty,ApplicationUserId,PaymentTypeId,Tag,Earnings,Id")] Record recordIM)
        {
            if (id != recordIM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid
                || (ModelState.ErrorCount == 1
                    && ModelState.GetValidationState(nameof(Record.ApplicationUserId)) == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    )
            {
                try
                {
                    var record = dataLayer.GetUserRecord(ApplicationUser, id);
                    
                    if (record == null)
                    {
                        return NotFound();
                    }

                    //record.Category = recordIM.Category;
                    record.CategoryId = recordIM.CategoryId;
                    record.Cost = recordIM.Cost;
                    record.Date = recordIM.Date;
                    record.Description = recordIM.Description;
                    record.Earnings = recordIM.Earnings;
                    //record.PaymentType = recordIM.PaymentType;
                    record.PaymentTypeId = recordIM.PaymentTypeId;
                    record.Tag = recordIM.Tag;
                    record.Warranty = recordIM.Warranty;

                    dataLayer.UpdateAsync(record);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!dataLayer.UserRecordExists(ApplicationUser, recordIM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", record.ApplicationUserId);
            ViewData["CategoryId"] = new SelectList(dataLayer.GetUserCategories(ApplicationUser), "Id", $"{nameof(Category.Description)}", recordIM.CategoryId);
            ViewData["PaymentTypeId"] = new SelectList(dataLayer.GetUserPaymentTypes(ApplicationUser), "Id", $"{nameof(PaymentType.Description)}", recordIM.PaymentTypeId);
            return View(recordIM);
        }

        // GET: Records/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = dataLayer.GetUserRecord(ApplicationUser, id);
            if (record == null)
            {
                return NotFound();
            }

            //dataLoader.Load(productType, pt => pt.Segment);
            dataLayer.Load(new List<Record> { record }, r => r.Category);
            dataLayer.Load(new List<Record> { record }, r => r.PaymentType);

            return View(record);
        }

        // POST: Records/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var record = dataLayer.GetUserRecord(ApplicationUser, id);

            if (record == null)
            {
                return NotFound();
            }

            dataLayer.DeleteAsync(record);

            return RedirectToAction(nameof(Index));
        }

        //https://stackoverflow.com/questions/20766306/calling-a-c-sharp-function-by-a-html-button-in-asp-net-razor
        //https://www.codeproject.com/Questions/316663/How-to-add-event-handler-to-a-button-in-mvc-razo
        public ActionResult ExportClick()
        {
            string message = "Welcome";
            //return new JsonResult( { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return new JsonResult(message);
        }

        //private bool RecordExists(string id)
        //{
        //    return _context.Records.Any(e => e.Id == id);
        //}

        //private Record GetUsersRecord(string id)
        //{
        //    return _context.Records
        //        .Include(r => r.ApplicationUser)
        //        .Include(r => r.Category)
        //        .Include(r => r.PaymentType)
        //        .SingleOrDefaultAsync(r => r.Id == id && r.ApplicationUser == this.ApplicationUser).Result;
        //}

        //private IQueryable<Record> GetUsersRecords()
        //{
        //    return _context.Records
        //        .Include(r => r.ApplicationUser)
        //        .Include(r => r.Category)
        //        .Include(r => r.PaymentType)
        //        .Where(r => r.ApplicationUser == this.ApplicationUser);
        //}
    }
}

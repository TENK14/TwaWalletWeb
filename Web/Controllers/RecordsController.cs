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

namespace TwaWallet.Web.Controllers
{
    [Authorize]
    //[Route("[controller]/[action]")]
    public class RecordsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDataLayer _dataLayer;

        public int MyProperty { get; set; }

        private ApplicationUser ApplicationUser
        {
            get
            {
                return _userManager.GetUserAsync(User).Result;
            }
        }

        public RecordsController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IDataLayer dataLayer)
        {
            _context = context;
            _userManager = userManager;
            _dataLayer = dataLayer;
        }
        
        // GET: Records
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = GetUsersRecords();
            var records = _dataLayer.GetUserRecords();

            if (records == null
                || !records.Any())
            {
                //return NotFound();
            }

            _dataLayer.Load(records, r => r.Category);
            _dataLayer.Load(records, r => r.PaymentType);

            return View(await records.ToListAsync());

        }
        
        // GET: Records/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = _dataLayer.GetUserRecord(id);

            if (record == null)
            {
                return NotFound();
            }

            _dataLayer.Load(new List<Record> { record }, r => r.Category);
            _dataLayer.Load(new List<Record> { record }, r => r.PaymentType);

            return View(record);
        }

        // GET: Records/Create
        public IActionResult Create()
        {
            //ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", $"{nameof(ApplicationUser.UserName)}");
            // TODO: zapoznamkuj
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", $"{nameof(Category.Description)}");
            //ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "Id", $"{nameof(PaymentType.Description)}");

            var record = new Record();

            //ViewData["CategoryId"] = new SelectList(_dataLayer.GetUserCategories(), "Id", $"{nameof(Category.Description)}");
            //ViewData["PaymentTypeId"] = new SelectList(_dataLayer.GetUserPaymentTypes(), "Id", $"{nameof(PaymentType.Description)}");
            ViewBag.CategoryId = new SelectList(_dataLayer.GetUserCategories().OrderByDescending(c => c.IsDefault), $"{nameof(Category.Id)}", $"{nameof(Category.Description)}");
            ViewBag.PaymentTypeId = new SelectList(_dataLayer.GetUserPaymentTypes().OrderByDescending(p => p.IsDefault), $"{nameof(PaymentType.Id)}", $"{nameof(PaymentType.Description)}");

            //ViewBag.CategoryId = new SelectList(db.Categories.OrderByDescending(c => c.IsDefault), "Id", "Description");
            //ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes.OrderByDescending(p => p.IsDefault), "Id", "Description");
            //ViewBag.LoginAccountId = new SelectList(db.LoginAccounts, "Id", "Username"); // TODO: napric celym systemem bude loginAccount = prihlaseny User a pak tato polozka bude skryta (preddefinovana)
            //ViewBag.Date = DateTime.Now;

            ViewBag.Date = DateTime.Now;

            return View(record);
        }

        // GET: Records/Create
        public IActionResult Create2()
        {
            //ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", $"{nameof(ApplicationUser.UserName)}");
            // TODO: zapoznamkuj
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", $"{nameof(Category.Description)}");
            //ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "Id", $"{nameof(PaymentType.Description)}");

            var record = new Record();

            //ViewData["CategoryId"] = new SelectList(_dataLayer.GetUserCategories(), "Id", $"{nameof(Category.Description)}");
            //ViewData["PaymentTypeId"] = new SelectList(_dataLayer.GetUserPaymentTypes(), "Id", $"{nameof(PaymentType.Description)}");
            ViewBag.CategoryId = new SelectList(_dataLayer.GetUserCategories().OrderByDescending(c => c.IsDefault), $"{nameof(Category.Id)}", $"{nameof(Category.Description)}");
            ViewBag.PaymentTypeId = new SelectList(_dataLayer.GetUserPaymentTypes().OrderByDescending(p => p.IsDefault), $"{nameof(PaymentType.Id)}", $"{nameof(PaymentType.Description)}");

            //ViewBag.CategoryId = new SelectList(db.Categories.OrderByDescending(c => c.IsDefault), "Id", "Description");
            //ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes.OrderByDescending(p => p.IsDefault), "Id", "Description");
            //ViewBag.LoginAccountId = new SelectList(db.LoginAccounts, "Id", "Username"); // TODO: napric celym systemem bude loginAccount = prihlaseny User a pak tato polozka bude skryta (preddefinovana)
            //ViewBag.Date = DateTime.Now;

            ViewBag.Date = DateTime.Now;

            return View(record);
        }

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
                var user = await _userManager.GetUserAsync(User);

                recordIM.ApplicationUser = user;

                _context.Add(recordIM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", $"{nameof(ApplicationUser.UserName)}");
            ViewData["CategoryId"] = new SelectList(_dataLayer.GetUserCategories(), "Id", $"{nameof(Category.Description)}", recordIM.Category);
            ViewData["PaymentTypeId"] = new SelectList(_dataLayer.GetUserPaymentTypes(), "Id", $"{nameof(PaymentType.Description)}");
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

            var record = _dataLayer.GetUserRecord(id);
            if (record == null)
            {
                return NotFound();
            }

            _dataLayer.Load(new List<Record> { record }, r => r.Category);
            _dataLayer.Load(new List<Record> { record }, r => r.PaymentType);

            //ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", record.ApplicationUserId);
            ViewData["CategoryId"] = new SelectList(_dataLayer.GetUserCategories(), "Id", $"{nameof(Category.Description)}", record.CategoryId);
            ViewData["PaymentTypeId"] = new SelectList(_dataLayer.GetUserPaymentTypes(), "Id", $"{nameof(PaymentType.Description)}", record.PaymentTypeId);
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
                    var record = _dataLayer.GetUserRecord(id);

                    if (record == null)
                    {
                        return NotFound();
                    }

                    record.Category = recordIM.Category;
                    record.Cost = recordIM.Cost;
                    record.Date = recordIM.Date;
                    record.Description = recordIM.Description;
                    record.Earnings = recordIM.Earnings;
                    record.PaymentType = recordIM.PaymentType;
                    record.Tag = recordIM.Tag;
                    record.Warranty = recordIM.Warranty;

                    _context.Update(recordIM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dataLayer.UserRecordExists(recordIM.Id))
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
            ViewData["CategoryId"] = new SelectList(_dataLayer.GetUserCategories(), "Id", $"{nameof(Category.Description)}", recordIM.CategoryId);
            ViewData["PaymentTypeId"] = new SelectList(_dataLayer.GetUserPaymentTypes(), "Id", $"{nameof(PaymentType.Description)}", recordIM.PaymentTypeId);
            return View(recordIM);
        }

        // GET: Records/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = _dataLayer.GetUserRecord(id);
            if (record == null)
            {
                return NotFound();
            }

            //dataLoader.Load(productType, pt => pt.Segment);
            _dataLayer.Load(new List<Record> { record }, r => r.Category);
            _dataLayer.Load(new List<Record> { record }, r => r.PaymentType);

            return View(record);
        }

        // POST: Records/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var record = _dataLayer.GetUserRecord(id);

            if (record == null)
            {
                return NotFound();
            }

            _context.Records.Remove(record);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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

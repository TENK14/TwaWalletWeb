using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TwaWallet.Entity;
using TwaWallet.Model;
using TwaWallet.Web.Controllers;
using TwaWallet.Web.DataLayer;
using TwaWallet.Web.Models.RecurringPaymentViewModels;

namespace Web.Controllers
{
    [Authorize]
    //[Route("[controller]/[action]")]
    public class RecurringPaymentsController : Controller
    {
        //private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDataLayer dataLayer;
        //private readonly ApplicationUser user;

        private ApplicationUser ApplicationUser
        {
            get
            {
                return userManager.GetUserAsync(User).Result;
            }
        }

        public RecurringPaymentsController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IDataLayer dataLayer)
        {
            //this.context = context;
            this.userManager = userManager;
            this.dataLayer = dataLayer;
            //this.user = userManager.GetUserAsync(User).Result;
        }

        // GET: RecurringPayments
        public async Task<IActionResult> Index()
        {

            var recurringPayments = dataLayer.GetUserRecurringPayments(ApplicationUser);
            dataLayer.Load(recurringPayments, rp => rp.Category);
            dataLayer.Load(recurringPayments, rp => rp.Interval);
            dataLayer.Load(recurringPayments, rp => rp.PaymentType);

            var categories = dataLayer.GetUserCategories(ApplicationUser).OrderByDescending(c => c.IsDefault);
            var intervals = dataLayer.GetIntervals().OrderByDescending(c => c.IsDefault);
            var paymentTypes = dataLayer.GetUserPaymentTypes(ApplicationUser).OrderByDescending(c => c.IsDefault);

            var recurringPaymentVMs = recurringPayments.ToList().Select(rp => ModelToVm(rp, new RecurringPaymentViewModel(), intervals.ToList())).ToList();

            //return View(await recurringPayments.ToListAsync());
            return View(recurringPaymentVMs);
        }

        // GET: RecurringPayments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recurringPayment = dataLayer.GetUserRecurringPayment(ApplicationUser, id);
            
            if (recurringPayment == null)
            {
                return NotFound();
            }

            var categories = dataLayer.GetUserCategories(ApplicationUser).OrderByDescending(c => c.IsDefault);
            var intervals = dataLayer.GetIntervals().OrderByDescending(c => c.IsDefault);
            var paymentTypes = dataLayer.GetUserPaymentTypes(ApplicationUser).OrderByDescending(c => c.IsDefault);

            dataLayer.Load(recurringPayment, rp => rp.Category);
            dataLayer.Load(recurringPayment, rp => rp.Interval);
            dataLayer.Load(recurringPayment, rp => rp.PaymentType);

            var recurringPaymentVM = new RecurringPaymentViewModel();
            ModelToVm(recurringPayment, recurringPaymentVM, intervals.ToList());

            return View(recurringPaymentVM);
        }

        // GET: RecurringPayments/Create
        public IActionResult Create()
        {
            var categories = dataLayer.GetUserCategories(ApplicationUser).OrderByDescending(c => c.IsDefault);
            var intervals = dataLayer.GetIntervals().OrderByDescending(c => c.IsDefault);
            var paymentTypes = dataLayer.GetUserPaymentTypes(ApplicationUser).OrderByDescending(c => c.IsDefault);

            ViewBag.CategoryId = new SelectList(categories, $"{nameof(Category.Id)}", $"{nameof(Category.Description)}");
            ViewBag.IntervalId = new SelectList(intervals, $"{nameof(Interval.Id)}", $"{nameof(Interval.Description)}");
            ViewBag.PaymentTypeId = new SelectList(paymentTypes, $"{nameof(PaymentType.Id)}", $"{nameof(PaymentType.Description)}");

            // alternativa
            //ViewData["CategoryId"] = new SelectList(_dataLayer.GetUserCategories().OrderByDescending(c => c.IsDefault), $"{nameof(Category.Id)}", $"{nameof(Category.Description)}");
            //ViewBag.PaymentTypeId = new SelectList(dataLayer.GetUserPaymentTypes().OrderByDescending(p => p.IsDefault), $"{nameof(PaymentType.Id)}", $"{nameof(PaymentType.Description)}");

            ViewBag.Date = DateTime.Now;

            var recurringPaymentVM = new RecurringPaymentViewModel();

            return View(recurringPaymentVM);
        }

        // POST: RecurringPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Description,CategoryId,PaymentTypeId,ApplicationUserId,Cost,IntervalId,Earnings,Warranty,Tag,DateCreated,LastUpdate,IsActive,EndDate,Id")] RecurringPayment recurringPayment)
        public async Task<IActionResult> Create([Bind("Description,CategoryId,PaymentTypeId,Cost,IntervalId,Earnings,Warranty,Tag,IsActive,StartDate,EndDate")] RecurringPaymentViewModel recurringPaymentVM)
        {
            var categories = dataLayer.GetUserCategories(ApplicationUser).OrderByDescending(c => c.IsDefault);
            var intervals = dataLayer.GetIntervals().OrderByDescending(c => c.IsDefault);
            var paymentTypes = dataLayer.GetUserPaymentTypes(ApplicationUser).OrderByDescending(c => c.IsDefault);

            if (ModelState.IsValid)
            {
                var recurringPayment = new RecurringPayment();
                VmToModel(recurringPaymentVM, recurringPayment, intervals.ToList());

                await dataLayer.AddAsync(recurringPayment);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(categories, $"{nameof(Category.Id)}", $"{nameof(Category.Description)}", recurringPaymentVM.CategoryId);
            ViewBag.IntervalId = new SelectList(intervals, $"{nameof(Interval.Id)}", $"{nameof(Interval.Description)}", recurringPaymentVM.IntervalId);
            ViewBag.PaymentTypeId = new SelectList(paymentTypes, $"{nameof(PaymentType.Id)}", $"{nameof(PaymentType.Description)}", recurringPaymentVM.PaymentTypeId);

            return View(recurringPaymentVM);
        }

        // GET: RecurringPayments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recurringPayment = dataLayer.GetUserRecurringPayment(ApplicationUser, id);
            if (recurringPayment == null)
            {
                return NotFound();
            }

            var categories = dataLayer.GetUserCategories(ApplicationUser).OrderByDescending(c => c.IsDefault);
            var intervals = dataLayer.GetIntervals().OrderByDescending(c => c.IsDefault);
            var paymentTypes = dataLayer.GetUserPaymentTypes(ApplicationUser).OrderByDescending(c => c.IsDefault);

            ViewBag.CategoryId = new SelectList(categories, $"{nameof(Category.Id)}", $"{nameof(Category.Description)}", recurringPayment.CategoryId);
            ViewBag.IntervalId = new SelectList(intervals, $"{nameof(Interval.Id)}", $"{nameof(Interval.Description)}", recurringPayment.IntervalId);
            ViewBag.PaymentTypeId = new SelectList(paymentTypes, $"{nameof(PaymentType.Id)}", $"{nameof(PaymentType.Description)}", recurringPayment.PaymentTypeId);

            var recurringPaymentVM = new RecurringPaymentViewModel();
            ModelToVm(recurringPayment, recurringPaymentVM, intervals.ToList());

            return View(recurringPaymentVM);
        }

        // POST: RecurringPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("Description,CategoryId,PaymentTypeId,ApplicationUserId,Cost,IntervalId,Earnings,Warranty,Tag,DateCreated,LastUpdate,IsActive,EndDate,Id")] RecurringPayment recurringPayment)
        public async Task<IActionResult> Edit(string id, [Bind("Description,CategoryId,PaymentTypeId,ApplicationUserId,Cost,IntervalId,Earnings,Warranty,Tag,IsActive,StartDate,EndDate,Id")] RecurringPaymentViewModel recurringPaymentVM)
        {
            if (id != recurringPaymentVM.Id)
            {
                return NotFound();
            }

             var categories = dataLayer.GetUserCategories(ApplicationUser).OrderByDescending(c => c.IsDefault);
             var intervals = dataLayer.GetIntervals().OrderByDescending(c => c.IsDefault);
             var paymentTypes = dataLayer.GetUserPaymentTypes(ApplicationUser).OrderByDescending(c => c.IsDefault);

            if (ModelState.IsValid)
            {
                try
                {
                    var recurringPayment = dataLayer.GetUserRecurringPayment(ApplicationUser, recurringPaymentVM.Id);
                    VmToModel(recurringPaymentVM, recurringPayment, intervals.ToList());

                    await dataLayer.UpdateAsync(recurringPayment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecurringPaymentExists(recurringPaymentVM.Id))
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

            ViewBag.CategoryId = new SelectList(categories, $"{nameof(Category.Id)}", $"{nameof(Category.Description)}", recurringPaymentVM.CategoryId);
            ViewBag.IntervalId = new SelectList(intervals, $"{nameof(Interval.Id)}", $"{nameof(Interval.Description)}", recurringPaymentVM.IntervalId);
            ViewBag.PaymentTypeId = new SelectList(paymentTypes, $"{nameof(PaymentType.Id)}", $"{nameof(PaymentType.Description)}", recurringPaymentVM.PaymentTypeId);
            
            return View(recurringPaymentVM);
        }

        // GET: RecurringPayments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recurringPayment = dataLayer.GetUserRecurringPayment(ApplicationUser, id);
            if (recurringPayment == null)
            {
                return NotFound();
            }

            dataLayer.Load(recurringPayment, rp => rp.Category);
            dataLayer.Load(recurringPayment, rp => rp.Interval);
            dataLayer.Load(recurringPayment, rp => rp.PaymentType);

            var intervals = dataLayer.GetIntervals().OrderByDescending(c => c.IsDefault);

            var recurringPaymentVM = new RecurringPaymentViewModel();
            ModelToVm(recurringPayment, recurringPaymentVM, intervals.ToList());


            return View(recurringPaymentVM);
        }

        // POST: RecurringPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var recurringPayment = dataLayer.GetUserRecurringPayment(ApplicationUser, id);
            await dataLayer.DeleteAsync(recurringPayment);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> GeneratePayment(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recurringPayment = dataLayer.GetUserRecurringPayment(ApplicationUser, id);
            if (recurringPayment == null)
            {
                return NotFound();
            }

            var categories = dataLayer.GetUserCategories(ApplicationUser).OrderByDescending(c => c.IsDefault);
            var intervals = dataLayer.GetIntervals().OrderByDescending(c => c.IsDefault);
            var paymentTypes = dataLayer.GetUserPaymentTypes(ApplicationUser).OrderByDescending(c => c.IsDefault);

            ViewBag.CategoryId = new SelectList(categories, $"{nameof(Category.Id)}", $"{nameof(Category.Description)}", recurringPayment.CategoryId);
            ViewBag.IntervalId = new SelectList(intervals, $"{nameof(Interval.Id)}", $"{nameof(Interval.Description)}", recurringPayment.IntervalId);
            ViewBag.PaymentTypeId = new SelectList(paymentTypes, $"{nameof(PaymentType.Id)}", $"{nameof(PaymentType.Description)}", recurringPayment.PaymentTypeId);

            var recurringPaymentVM = new RecurringPaymentViewModel();
            ModelToVm(recurringPayment, recurringPaymentVM, intervals.ToList());

            var record = new TwaWallet.Model.Record
            {
                ApplicationUser = await userManager.GetUserAsync(User),
                CategoryId = recurringPayment.CategoryId,
                Cost = recurringPayment.Cost,
                Date = DateTime.Now,
                Description = recurringPayment.Description,
                Earnings = recurringPayment.Earnings,
                PaymentTypeId = recurringPayment.PaymentTypeId,
                Tag = recurringPayment.Tag,
                Warranty = recurringPayment.Warranty
            };
            await dataLayer.AddAsync(record);
            return RedirectToAction(nameof(Index));
            //return RedirectToAction(nameof(Index), "Records");

            //return RedirectToAction("Create", "Records", new TwaWallet.Model.Record
            //{
            //    //ApplicationUserId = Model.apli,
            //    CategoryId = recurringPayment.CategoryId,
            //    Cost = recurringPayment.Cost,
            //    Date = DateTime.Now.AddDays(-2),
            //    Description = recurringPayment.Description,
            //    Earnings = recurringPayment.Earnings,
            //    PaymentTypeId = recurringPayment.PaymentTypeId,
            //    Tag = recurringPayment.Tag,
            //    Warranty = recurringPayment.Warranty
            //});

            //return new RecordsController(userManager, dataLayer).SetCreate(record);

            //return View(recurringPaymentVM);
        }

        private bool RecurringPaymentExists(string id)
        {
            return dataLayer.UserRecurringPaymentExists(ApplicationUser, id);
        }

        private void VmToModel(RecurringPaymentViewModel recurringPaymentVM, RecurringPayment recurringPayment, List<Interval> intervals)
        {
            recurringPayment.ApplicationUser = ApplicationUser;
            recurringPayment.CategoryId = recurringPaymentVM.CategoryId;
            recurringPayment.Cost = recurringPaymentVM.Cost;
            recurringPayment.Description = recurringPaymentVM.Description;
            recurringPayment.Earnings = recurringPaymentVM.Earnings;
            recurringPayment.EndDate = recurringPaymentVM.EndDate;
            recurringPayment.IntervalId = recurringPaymentVM.IntervalId;
            recurringPayment.IsActive = recurringPaymentVM.IsActive;
            recurringPayment.PaymentTypeId = recurringPaymentVM.PaymentTypeId;
            recurringPayment.LastUpdate = intervals.Single(i => i.Id == recurringPaymentVM.IntervalId).BeforeDateTime(recurringPaymentVM.NextDate);
            recurringPayment.Tag = recurringPaymentVM.Tag;
            recurringPayment.Warranty = recurringPaymentVM.Warranty;
        }

        private RecurringPaymentViewModel ModelToVm(RecurringPayment recurringPayment, RecurringPaymentViewModel recurringPaymentVM, List<Interval> intervals)
        {
            dataLayer.Load(recurringPayment, rp => rp.Category);
            dataLayer.Load(recurringPayment, rp => rp.Interval);
            dataLayer.Load(recurringPayment, rp => rp.PaymentType);

            //recurringPaymentVM.ApplicationUser = ApplicationUser;
            recurringPaymentVM.Id = recurringPayment.Id;
            recurringPaymentVM.CategoryId = recurringPayment.CategoryId;
            recurringPaymentVM.CategoryDescription = recurringPayment.Category.Description;
            recurringPaymentVM.Cost = recurringPayment.Cost;
            recurringPaymentVM.Description = recurringPayment.Description;
            recurringPaymentVM.Earnings = recurringPayment.Earnings;
            recurringPaymentVM.EndDate = recurringPayment.EndDate;
            recurringPaymentVM.IntervalId = recurringPayment.IntervalId;
            recurringPaymentVM.IntervalDescription = recurringPayment.Interval.Description;
            recurringPaymentVM.IsActive = recurringPayment.IsActive;
            recurringPaymentVM.PaymentTypeId = recurringPayment.PaymentTypeId;
            recurringPaymentVM.PaymentTypeDescription = recurringPayment.PaymentType.Description;
            recurringPaymentVM.LastUpdate = recurringPayment.LastUpdate;
            recurringPaymentVM.NextDate = intervals.Single(i => i.Id == recurringPayment.IntervalId).NextDateTime(recurringPayment.LastUpdate);
            recurringPaymentVM.Tag = recurringPayment.Tag;
            recurringPaymentVM.Warranty = recurringPayment.Warranty;
            recurringPaymentVM.DateCreated = recurringPayment.DateCreated;

            return recurringPaymentVM;
        }
    }
}

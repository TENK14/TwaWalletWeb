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
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<RecurringPaymentsController> logger;
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
            IDataLayer dataLayer,
            ILogger<RecurringPaymentsController> logger)
        {
            //this.context = context;
            this.userManager = userManager;
            this.dataLayer = dataLayer;
            this.logger = logger;
            //this.user = userManager.GetUserAsync(User).Result;
        }

        // GET: RecurringPayments
        public async Task<IActionResult> Index()
        {
            try
            {
                var user = ApplicationUser;
                if (user == null)
                {
                    logger.LogError("ApplicationUser is null in RecurringPaymentsController.Index. User.Identity.Name: {UserName}, User.Identity.IsAuthenticated: {IsAuthenticated}",
                        User?.Identity?.Name ?? "null",
                        User?.Identity?.IsAuthenticated ?? false);
                    return RedirectToAction("Login", "Account");
                }

                var recurringPayments = dataLayer.GetUserRecurringPayments(user).ToList();
                dataLayer.Load(recurringPayments, rp => rp.Category);
                dataLayer.Load(recurringPayments, rp => rp.Interval);
                dataLayer.Load(recurringPayments, rp => rp.PaymentType);

                var categories = dataLayer.GetUserCategories(user).OrderByDescending(c => c.IsDefault);
                var intervals = dataLayer.GetIntervals().OrderByDescending(c => c.IsDefault);
                var paymentTypes = dataLayer.GetUserPaymentTypes(user).OrderByDescending(c => c.IsDefault);

                var recurringPaymentVMs = recurringPayments.ToList().Select(rp => ModelToVm(rp, new RecurringPaymentViewModel(), intervals.ToList())).ToList();

                //return View(await recurringPayments.ToListAsync());
                return View(recurringPaymentVMs);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred in RecurringPaymentsController.Index for user {UserName}", User?.Identity?.Name ?? "unknown");
                return View("Error");
            }
        }

        // GET: RecurringPayments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                if (id == null)
                {
                    logger.LogWarning("Details called with null id");
                    return NotFound();
                }

                var user = ApplicationUser;
                if (user == null)
                {
                    logger.LogError("ApplicationUser is null in RecurringPaymentsController.Details. User: {UserName}", User?.Identity?.Name ?? "null");
                    return RedirectToAction("Login", "Account");
                }

                var recurringPayment = dataLayer.GetUserRecurringPayment(user, id);
                
                if (recurringPayment == null)
                {
                    logger.LogWarning("RecurringPayment with id {Id} not found for user {UserName}", id, user.UserName);
                    return NotFound();
                }

                var categories = dataLayer.GetUserCategories(user).OrderByDescending(c => c.IsDefault);
                var intervals = dataLayer.GetIntervals().OrderByDescending(c => c.IsDefault);
                var paymentTypes = dataLayer.GetUserPaymentTypes(user).OrderByDescending(c => c.IsDefault);

                dataLayer.Load(recurringPayment, rp => rp.Category);
                dataLayer.Load(recurringPayment, rp => rp.Interval);
                dataLayer.Load(recurringPayment, rp => rp.PaymentType);

                var recurringPaymentVM = new RecurringPaymentViewModel();
                ModelToVm(recurringPayment, recurringPaymentVM, intervals.ToList());

                return View(recurringPaymentVM);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred in RecurringPaymentsController.Details for id {Id}, user {UserName}", id, User?.Identity?.Name ?? "unknown");
                return View("Error");
            }
        }

        // GET: RecurringPayments/Create
        public IActionResult Create()
        {
            try
            {
                var user = ApplicationUser;
                if (user == null)
                {
                    logger.LogError("ApplicationUser is null in RecurringPaymentsController.Create");
                    return RedirectToAction("Login", "Account");
                }

                var categories = dataLayer.GetUserCategories(user).OrderByDescending(c => c.IsDefault);
                var intervals = dataLayer.GetIntervals().OrderByDescending(c => c.IsDefault);
                var paymentTypes = dataLayer.GetUserPaymentTypes(user).OrderByDescending(c => c.IsDefault);

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
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred in RecurringPaymentsController.Create GET for user {UserName}", User?.Identity?.Name ?? "unknown");
                return View("Error");
            }
        }

        // POST: RecurringPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Description,CategoryId,PaymentTypeId,ApplicationUserId,Cost,IntervalId,Earnings,Warranty,Tag,DateCreated,LastUpdate,IsActive,EndDate,Id")] RecurringPayment recurringPayment)
        public async Task<IActionResult> Create([Bind("Description,CategoryId,PaymentTypeId,Cost,IntervalId,Earnings,Warranty,Tag,IsActive,StartDate,EndDate")] RecurringPaymentViewModel recurringPaymentVM)
        {
            try
            {
                var user = ApplicationUser;
                if (user == null)
                {
                    logger.LogError("ApplicationUser is null in RecurringPaymentsController.Create POST");
                    return RedirectToAction("Login", "Account");
                }

                var categories = dataLayer.GetUserCategories(user).OrderByDescending(c => c.IsDefault);
                var intervals = dataLayer.GetIntervals().OrderByDescending(c => c.IsDefault);
                var paymentTypes = dataLayer.GetUserPaymentTypes(user).OrderByDescending(c => c.IsDefault);

                if (ModelState.IsValid)
                {
                    var recurringPayment = new RecurringPayment();
                    VmToModel(recurringPaymentVM, recurringPayment, intervals.ToList());

                    await dataLayer.AddAsync(recurringPayment);
                    logger.LogInformation("RecurringPayment created successfully by user {UserName}", user.UserName);
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.CategoryId = new SelectList(categories, $"{nameof(Category.Id)}", $"{nameof(Category.Description)}", recurringPaymentVM.CategoryId);
                ViewBag.IntervalId = new SelectList(intervals, $"{nameof(Interval.Id)}", $"{nameof(Interval.Description)}", recurringPaymentVM.IntervalId);
                ViewBag.PaymentTypeId = new SelectList(paymentTypes, $"{nameof(PaymentType.Id)}", $"{nameof(PaymentType.Description)}", recurringPaymentVM.PaymentTypeId);

                return View(recurringPaymentVM);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred in RecurringPaymentsController.Create POST for user {UserName}", User?.Identity?.Name ?? "unknown");
                return View("Error");
            }
        }

        // GET: RecurringPayments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                if (id == null)
                {
                    logger.LogWarning("Edit called with null id");
                    return NotFound();
                }

                var user = ApplicationUser;
                if (user == null)
                {
                    logger.LogError("ApplicationUser is null in RecurringPaymentsController.Edit GET");
                    return RedirectToAction("Login", "Account");
                }

                var recurringPayment = dataLayer.GetUserRecurringPayment(user, id);
                if (recurringPayment == null)
                {
                    logger.LogWarning("RecurringPayment with id {Id} not found for user {UserName}", id, user.UserName);
                    return NotFound();
                }

                var categories = dataLayer.GetUserCategories(user).OrderByDescending(c => c.IsDefault);
                var intervals = dataLayer.GetIntervals().OrderByDescending(c => c.IsDefault);
                var paymentTypes = dataLayer.GetUserPaymentTypes(user).OrderByDescending(c => c.IsDefault);

                ViewBag.CategoryId = new SelectList(categories, $"{nameof(Category.Id)}", $"{nameof(Category.Description)}", recurringPayment.CategoryId);
                ViewBag.IntervalId = new SelectList(intervals, $"{nameof(Interval.Id)}", $"{nameof(Interval.Description)}", recurringPayment.IntervalId);
                ViewBag.PaymentTypeId = new SelectList(paymentTypes, $"{nameof(PaymentType.Id)}", $"{nameof(PaymentType.Description)}", recurringPayment.PaymentTypeId);

                var recurringPaymentVM = new RecurringPaymentViewModel();
                ModelToVm(recurringPayment, recurringPaymentVM, intervals.ToList());

                return View(recurringPaymentVM);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred in RecurringPaymentsController.Edit GET for id {Id}, user {UserName}", id, User?.Identity?.Name ?? "unknown");
                return View("Error");
            }
        }

        // POST: RecurringPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("Description,CategoryId,PaymentTypeId,ApplicationUserId,Cost,IntervalId,Earnings,Warranty,Tag,DateCreated,LastUpdate,IsActive,EndDate,Id")] RecurringPayment recurringPayment)
        public async Task<IActionResult> Edit(string id, [Bind("Description,CategoryId,PaymentTypeId,ApplicationUserId,Cost,IntervalId,Earnings,Warranty,Tag,IsActive,StartDate,EndDate,Id")] RecurringPaymentViewModel recurringPaymentVM)
        {
            try
            {
                if (id != recurringPaymentVM.Id)
                {
                    logger.LogWarning("Edit POST id mismatch. URL id: {UrlId}, ViewModel id: {ViewModelId}", id, recurringPaymentVM.Id);
                    return NotFound();
                }

                var user = ApplicationUser;
                if (user == null)
                {
                    logger.LogError("ApplicationUser is null in RecurringPaymentsController.Edit POST");
                    return RedirectToAction("Login", "Account");
                }

                var categories = dataLayer.GetUserCategories(user).OrderByDescending(c => c.IsDefault);
                var intervals = dataLayer.GetIntervals().OrderByDescending(c => c.IsDefault);
                var paymentTypes = dataLayer.GetUserPaymentTypes(user).OrderByDescending(c => c.IsDefault);

                if (ModelState.IsValid)
                {
                    try
                    {
                        var recurringPayment = dataLayer.GetUserRecurringPayment(user, recurringPaymentVM.Id);
                        VmToModel(recurringPaymentVM, recurringPayment, intervals.ToList());

                        await dataLayer.UpdateAsync(recurringPayment);
                        logger.LogInformation("RecurringPayment {Id} updated successfully by user {UserName}", id, user.UserName);
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        if (!RecurringPaymentExists(recurringPaymentVM.Id))
                        {
                            logger.LogWarning("RecurringPayment {Id} not found during update", recurringPaymentVM.Id);
                            return NotFound();
                        }
                        else
                        {
                            logger.LogError(ex, "DbUpdateConcurrencyException occurred while updating RecurringPayment {Id}", recurringPaymentVM.Id);
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
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred in RecurringPaymentsController.Edit POST for id {Id}, user {UserName}", id, User?.Identity?.Name ?? "unknown");
                return View("Error");
            }
        }

        // GET: RecurringPayments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (id == null)
                {
                    logger.LogWarning("Delete called with null id");
                    return NotFound();
                }

                var user = ApplicationUser;
                if (user == null)
                {
                    logger.LogError("ApplicationUser is null in RecurringPaymentsController.Delete GET");
                    return RedirectToAction("Login", "Account");
                }

                var recurringPayment = dataLayer.GetUserRecurringPayment(user, id);
                if (recurringPayment == null)
                {
                    logger.LogWarning("RecurringPayment with id {Id} not found for deletion", id);
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
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred in RecurringPaymentsController.Delete GET for id {Id}, user {UserName}", id, User?.Identity?.Name ?? "unknown");
                return View("Error");
            }
        }

        // POST: RecurringPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var user = ApplicationUser;
                if (user == null)
                {
                    logger.LogError("ApplicationUser is null in RecurringPaymentsController.DeleteConfirmed");
                    return RedirectToAction("Login", "Account");
                }

                var recurringPayment = dataLayer.GetUserRecurringPayment(user, id);
                await dataLayer.DeleteAsync(recurringPayment);
                logger.LogInformation("RecurringPayment {Id} deleted successfully by user {UserName}", id, user.UserName);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred in RecurringPaymentsController.DeleteConfirmed for id {Id}, user {UserName}", id, User?.Identity?.Name ?? "unknown");
                return View("Error");
            }
        }


        public async Task<IActionResult> GenerateAllActiveRecurringPayments()
        {
            try
            {
                var user = ApplicationUser;
                if (user == null)
                {
                    logger.LogError("ApplicationUser is null in RecurringPaymentsController.GenerateAllActiveRecurringPayments");
                    return RedirectToAction("Login", "Account");
                }

                var activeRecurringPayments = dataLayer.GetUserRecurringPayments(user)
                    .Where(rp => rp.IsActive);

                var records = new List<Record>();

                foreach(var recurringPayment in activeRecurringPayments)
                {
                    records.Add(await MapRecurringPaymentToRecordAsync(recurringPayment));
                }

                await dataLayer.AddRangeAsync(records.ToArray());
                logger.LogInformation("Generated {Count} payments from active recurring payments for user {UserName}", records.Count, user.UserName);

                return RedirectToAction(nameof(Index), "Records");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred in RecurringPaymentsController.GenerateAllActiveRecurringPayments for user {UserName}", User?.Identity?.Name ?? "unknown");
                return View("Error");
            }
        }


        public async Task<IActionResult> GeneratePayment(string id)
        {
            try
            {
                if (id == null)
                {
                    logger.LogWarning("GeneratePayment called with null id");
                    return NotFound();
                }

                var user = ApplicationUser;
                if (user == null)
                {
                    logger.LogError("ApplicationUser is null in RecurringPaymentsController.GeneratePayment");
                    return RedirectToAction("Login", "Account");
                }

                var recurringPayment = dataLayer.GetUserRecurringPayment(user, id);
                if (recurringPayment == null)
                {
                    logger.LogWarning("RecurringPayment with id {Id} not found for payment generation", id);
                    return NotFound();
                }

                var record = await MapRecurringPaymentToRecordAsync(recurringPayment);
                await dataLayer.AddAsync(record);
                logger.LogInformation("Payment generated from RecurringPayment {Id} for user {UserName}", id, user.UserName);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred in RecurringPaymentsController.GeneratePayment for id {Id}, user {UserName}", id, User?.Identity?.Name ?? "unknown");
                return View("Error");
            }
        }

        private async Task<Record> MapRecurringPaymentToRecordAsync(RecurringPayment recurringPayment)
        {
            var record = new Record
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

            return record;
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

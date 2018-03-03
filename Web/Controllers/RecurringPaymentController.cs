using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TwaWallet.Entity;
using TwaWallet.Model;

namespace TwaWallet.Web.Controllers
{
    public class RecurringPaymentController : Controller
    {
        private TwaWalletDbContext db = new TwaWalletDbContext();

        // GET: RecurringPayment
        public ActionResult Index()
        {
            var recurringPayments = db.RecurringPayments.Include(r => r.Category).Include(r => r.Interval).Include(r => r.PaymentType).Include(r => r.LoginAccount);
            return View(recurringPayments.ToList());
        }

        // GET: RecurringPayment/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecurringPayment recurringPayment = db.RecurringPayments.Include(r => r.Category).Include(r => r.Interval).Include(r => r.PaymentType).Include(r => r.LoginAccount).Single(r => r.Id == id);
            if (recurringPayment == null)
            {
                return HttpNotFound();
            }
            return View(recurringPayment);
        }

        // GET: RecurringPayment/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Description");
            ViewBag.IntervalId = new SelectList(db.Intervals, "Id", "Description");
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Description");
            ViewBag.UserId = new SelectList(db.LoginAccounts, "Id", "Username");
            return View();
        }

        // POST: RecurringPayment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,CategoryId,PaymentTypeId,UserId,Cost,IntervalId,Earnings,Warranty,Tag,DateCreated,LastUpdate,IsActive,EndDate")] RecurringPayment recurringPayment)
        {
            if (ModelState.IsValid)
            {
                db.RecurringPayments.Add(recurringPayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Description", recurringPayment.CategoryId);
            ViewBag.IntervalId = new SelectList(db.Intervals, "Id", "Description", recurringPayment.IntervalId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Description", recurringPayment.PaymentTypeId);
            ViewBag.UserId = new SelectList(db.LoginAccounts, "Id", "Username", recurringPayment.LoginAccountId);
            return View(recurringPayment);
        }

        // GET: RecurringPayment/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecurringPayment recurringPayment = db.RecurringPayments.Include(r => r.Category).Include(r => r.Interval).Include(r => r.PaymentType).Include(r => r.LoginAccount).Single(r => r.Id == id);
            if (recurringPayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Description", recurringPayment.CategoryId);
            ViewBag.IntervalId = new SelectList(db.Intervals, "Id", "Description", recurringPayment.IntervalId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Description", recurringPayment.PaymentTypeId);
            ViewBag.UserId = new SelectList(db.LoginAccounts, "Id", "Username", recurringPayment.LoginAccountId);
            return View(recurringPayment);
        }

        // POST: RecurringPayment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,CategoryId,PaymentTypeId,UserId,Cost,IntervalId,Earnings,Warranty,Tag,DateCreated,LastUpdate,IsActive,EndDate")] RecurringPayment recurringPayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recurringPayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Description", recurringPayment.CategoryId);
            ViewBag.IntervalId = new SelectList(db.Intervals, "Id", "Description", recurringPayment.IntervalId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Description", recurringPayment.PaymentTypeId);
            ViewBag.UserId = new SelectList(db.LoginAccounts, "Id", "Username", recurringPayment.LoginAccountId);
            return View(recurringPayment);
        }

        // GET: RecurringPayment/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecurringPayment recurringPayment = db.RecurringPayments.Include(r => r.Category).Include(r => r.Interval).Include(r => r.PaymentType).Include(r => r.LoginAccount).Single(r => r.Id == id);
            if (recurringPayment == null)
            {
                return HttpNotFound();
            }
            return View(recurringPayment);
        }

        // POST: RecurringPayment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            RecurringPayment recurringPayment = db.RecurringPayments.Find(id);
            db.RecurringPayments.Remove(recurringPayment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

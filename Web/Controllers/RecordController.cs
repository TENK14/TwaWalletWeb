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
    public class RecordController : Controller
    {
        private TwaWalletDbContext db = new TwaWalletDbContext();

        // GET: Record
        public ActionResult Index()
        {
            var records = db.Records.OrderByDescending(r => r.Date).Include(r => r.Category).Include(r => r.PaymentType).Include(r => r.User);
            return View(records.ToList());
        }

        // GET: Record/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        // GET: Record/Create
        public ActionResult Create()
        {
            var record = new Record();

            ViewBag.CategoryId = new SelectList(db.Categories.OrderByDescending(c => c.IsDefault), "Id", "Description");
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes.OrderByDescending(p => p.IsDefault), "Id", "Description");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name"); // TODO: napric celym systemem bude loginAccount = prihlaseny User a pak tato polozka bude skryta (preddefinovana)
                        
            return View(record);
        }

        // POST: Record/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cost,Description,CategoryId,Date,Warranty,UserId,PaymentTypeId,Tag,Earnings")] Record record)
        {
            if (ModelState.IsValid)
            {
                db.Records.Add(record);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Description", record.CategoryId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Description", record.PaymentTypeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", record.UserId);
            return View(record);
        }

        // GET: Record/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Description", record.CategoryId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Description", record.PaymentTypeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", record.UserId);
            return View(record);
        }

        // POST: Record/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cost,Description,CategoryId,Date,Warranty,UserId,PaymentTypeId,Tag,Earnings")] Record record)
        {
            if (ModelState.IsValid)
            {
                db.Entry(record).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Description", record.CategoryId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Description", record.PaymentTypeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", record.UserId);
            return View(record);
        }

        // GET: Record/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        // POST: Record/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Record record = db.Records.Find(id);
            db.Records.Remove(record);
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

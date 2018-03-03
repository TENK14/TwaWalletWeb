﻿using System;
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
    public class IntervalController : Controller
    {
        private TwaWalletDbContext db = new TwaWalletDbContext();

        // GET: Interval
        public ActionResult Index()
        {
            return View(db.Intervals.ToList());
        }

        // GET: Interval/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interval interval = db.Intervals.Find(id);
            if (interval == null)
            {
                return HttpNotFound();
            }
            return View(interval);
        }

        // GET: Interval/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Interval/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,IsDefault,IntervalCode")] Interval interval)
        {
            if (ModelState.IsValid)
            {
                db.Intervals.Add(interval);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(interval);
        }

        // GET: Interval/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interval interval = db.Intervals.Find(id);
            if (interval == null)
            {
                return HttpNotFound();
            }
            return View(interval);
        }

        // POST: Interval/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,IsDefault,IntervalCode")] Interval interval)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interval).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(interval);
        }

        // GET: Interval/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interval interval = db.Intervals.Find(id);
            if (interval == null)
            {
                return HttpNotFound();
            }
            return View(interval);
        }

        // POST: Interval/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Interval interval = db.Intervals.Find(id);
            db.Intervals.Remove(interval);
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

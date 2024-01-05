﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyApp.Core;

namespace WebApp1.Controllers
{
    public class VendorController : Controller
    {
        private WebDbContextEntities db = new WebDbContextEntities();

        // GET: tb_m_vendors
        public ActionResult Index()
        {
            var tb_m_vendors = db.tb_m_vendors.Include(t => t.tb_m_account_vendors);
            return View(tb_m_vendors.ToList());
        }

        // GET: tb_m_vendors/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_vendors tb_m_vendors = db.tb_m_vendors.Find(id);
            if (tb_m_vendors == null)
            {
                return HttpNotFound();
            }
            return View(tb_m_vendors);
        }

        // GET: tb_m_vendors/Create
        public ActionResult Create()
        {
            ViewBag.guid = new SelectList(db.tb_m_account_vendors, "guid", "password");
            return View();
        }

        // POST: tb_m_vendors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "guid,vendor_name,email,phone_number,photo_profile,sector,type,is_admin_approve,is_manager_approve,created_date,modified_date")] tb_m_vendors tb_m_vendors)
        {
            if (ModelState.IsValid)
            {
                tb_m_vendors.guid = Guid.NewGuid();
                db.tb_m_vendors.Add(tb_m_vendors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.guid = new SelectList(db.tb_m_account_vendors, "guid", "password", tb_m_vendors.guid);
            return View(tb_m_vendors);
        }

        // GET: tb_m_vendors/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_vendors tb_m_vendors = db.tb_m_vendors.Find(id);
            if (tb_m_vendors == null)
            {
                return HttpNotFound();
            }
            ViewBag.guid = new SelectList(db.tb_m_account_vendors, "guid", "password", tb_m_vendors.guid);
            return View(tb_m_vendors);
        }

        // POST: tb_m_vendors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "guid,vendor_name,email,phone_number,photo_profile,sector,type,is_admin_approve,is_manager_approve,created_date,modified_date")] tb_m_vendors tb_m_vendors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_m_vendors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.guid = new SelectList(db.tb_m_account_vendors, "guid", "password", tb_m_vendors.guid);
            return View(tb_m_vendors);
        }

        // GET: tb_m_vendors/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_vendors tb_m_vendors = db.tb_m_vendors.Find(id);
            if (tb_m_vendors == null)
            {
                return HttpNotFound();
            }
            return View(tb_m_vendors);
        }

        // POST: tb_m_vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            tb_m_vendors tb_m_vendors = db.tb_m_vendors.Find(id);
            db.tb_m_vendors.Remove(tb_m_vendors);
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

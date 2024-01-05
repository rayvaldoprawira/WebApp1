using System;
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
        /* private WebDbContextEntities db = new WebDbContextEntities();*/
        private readonly IVendorRepository _db;

        public VendorController(IVendorRepository db)
        {
            _db = db;
        }

        // GET: Vendor
        public ActionResult Index()
        {
            return View(_db.GetAll());
        }

        // GET: Vendor/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_vendors tb_m_vendors = _db.GetByGuid(id);
            if (tb_m_vendors == null)
            {
                return HttpNotFound();
            }
            return View(tb_m_vendors);
        }

        // GET: Vendor/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Vendor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "guid,vendor_name,email,phone_number,photo_profile,sector,type,is_admin_approve,is_manager_approve,created_date,modified_date")] tb_m_vendors tb_m_vendors)
        {
            if (ModelState.IsValid)
            {
                tb_m_vendors.guid = Guid.NewGuid();
                tb_m_vendors.created_date = DateTime.Now;
                tb_m_vendors.modified_date = DateTime.Now;
                _db.Create(tb_m_vendors);
                return RedirectToAction("Index");
            }

         
            return View(tb_m_vendors);
        }

        // GET: Vendor/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_vendors tb_m_vendors = _db.GetByGuid(id);
            if (tb_m_vendors == null)
            {
                return HttpNotFound();
            }
            return View(tb_m_vendors);
        }

        // POST: Vendor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "guid,vendor_name,email,phone_number,photo_profile,sector,type,is_admin_approve,is_manager_approve,created_date,modified_date")] tb_m_vendors tb_m_vendors)
        {
            if (ModelState.IsValid)
            {
                _db.Update(tb_m_vendors);
                return RedirectToAction("Index");
            }
          
            return View(tb_m_vendors);
        }

        // GET: Vendor/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_vendors tb_m_vendors = _db.GetByGuid(id);
            if (tb_m_vendors == null)
            {
                return HttpNotFound();
            }
            return View(tb_m_vendors);
        }

        // POST: Vendor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            tb_m_vendors tb_m_vendors = _db.GetByGuid(id);
            _db.Delete(tb_m_vendors.guid);
            return RedirectToAction("Index");
        }
/*
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}

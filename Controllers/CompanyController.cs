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
    public class CompanyController : Controller
    {
        private WebDbContextEntities db = new WebDbContextEntities();

        // GET: tb_m_companies
        public ActionResult Index()
        {
            var tb_m_companies = db.tb_m_companies.Include(t => t.tb_m_accounts);
            return View(tb_m_companies.ToList());
        }

        // GET: tb_m_companies/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_companies tb_m_companies = db.tb_m_companies.Find(id);
            if (tb_m_companies == null)
            {
                return HttpNotFound();
            }
            return View(tb_m_companies);
        }

        // GET: tb_m_companies/Create
        public ActionResult Create()
        {
            ViewBag.guid = new SelectList(db.tb_m_accounts, "guid", "password");
            return View();
        }

        // POST: tb_m_companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "guid,first_name,last_name,email,created_date,modified_date")] tb_m_companies tb_m_companies)
        {
            if (ModelState.IsValid)
            {
                tb_m_companies.guid = Guid.NewGuid();
                db.tb_m_companies.Add(tb_m_companies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.guid = new SelectList(db.tb_m_accounts, "guid", "password", tb_m_companies.guid);
            return View(tb_m_companies);
        }

        // GET: tb_m_companies/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_companies tb_m_companies = db.tb_m_companies.Find(id);
            if (tb_m_companies == null)
            {
                return HttpNotFound();
            }
            ViewBag.guid = new SelectList(db.tb_m_accounts, "guid", "password", tb_m_companies.guid);
            return View(tb_m_companies);
        }

        // POST: tb_m_companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "guid,first_name,last_name,email,created_date,modified_date")] tb_m_companies tb_m_companies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_m_companies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.guid = new SelectList(db.tb_m_accounts, "guid", "password", tb_m_companies.guid);
            return View(tb_m_companies);
        }

        // GET: tb_m_companies/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_companies tb_m_companies = db.tb_m_companies.Find(id);
            if (tb_m_companies == null)
            {
                return HttpNotFound();
            }
            return View(tb_m_companies);
        }

        // POST: tb_m_companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            tb_m_companies tb_m_companies = db.tb_m_companies.Find(id);
            db.tb_m_companies.Remove(tb_m_companies);
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

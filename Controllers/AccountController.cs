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
    public class AccountController : Controller
    {
        private WebDbContextEntities db = new WebDbContextEntities();

        // GET: Account
        public ActionResult Index()
        {
            var tb_m_accounts = db.tb_m_accounts.Include(t => t.tb_m_companies);
            return View(tb_m_accounts.ToList());
        }

        // GET: Account/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_accounts tb_m_accounts = db.tb_m_accounts.Find(id);
            if (tb_m_accounts == null)
            {
                return HttpNotFound();
            }
            return View(tb_m_accounts);
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            ViewBag.guid = new SelectList(db.tb_m_companies, "guid", "first_name");
            return View();
        }

        // POST: Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "guid,password,created_date,modified_date")] tb_m_accounts tb_m_accounts)
        {
            if (ModelState.IsValid)
            {
                tb_m_accounts.guid = Guid.NewGuid();
                db.tb_m_accounts.Add(tb_m_accounts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.guid = new SelectList(db.tb_m_companies, "guid", "first_name", tb_m_accounts.guid);
            return View(tb_m_accounts);
        }

        // GET: Account/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_accounts tb_m_accounts = db.tb_m_accounts.Find(id);
            if (tb_m_accounts == null)
            {
                return HttpNotFound();
            }
            ViewBag.guid = new SelectList(db.tb_m_companies, "guid", "first_name", tb_m_accounts.guid);
            return View(tb_m_accounts);
        }

        // POST: Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "guid,password,created_date,modified_date")] tb_m_accounts tb_m_accounts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_m_accounts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.guid = new SelectList(db.tb_m_companies, "guid", "first_name", tb_m_accounts.guid);
            return View(tb_m_accounts);
        }

        // GET: Account/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_accounts tb_m_accounts = db.tb_m_accounts.Find(id);
            if (tb_m_accounts == null)
            {
                return HttpNotFound();
            }
            return View(tb_m_accounts);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            tb_m_accounts tb_m_accounts = db.tb_m_accounts.Find(id);
            db.tb_m_accounts.Remove(tb_m_accounts);
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

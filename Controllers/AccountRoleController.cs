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
    public class AccountRoleController : Controller
    {
        private WebDbContextEntities db = new WebDbContextEntities();

        // GET: AccountRole
        public ActionResult Index()
        {
            var tb_tr_account_roles = db.tb_tr_account_roles.Include(t => t.tb_m_accounts).Include(t => t.tb_m_roles);
            return View(tb_tr_account_roles.ToList());
        }

        // GET: AccountRole/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_tr_account_roles tb_tr_account_roles = db.tb_tr_account_roles.Find(id);
            if (tb_tr_account_roles == null)
            {
                return HttpNotFound();
            }
            return View(tb_tr_account_roles);
        }

        // GET: AccountRole/Create
        public ActionResult Create()
        {
            ViewBag.account_guid = new SelectList(db.tb_m_accounts, "guid", "password");
            ViewBag.role_guid = new SelectList(db.tb_m_roles, "guid", "name");
            return View();
        }

        // POST: AccountRole/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "guid,account_guid,role_guid,created_date,modified_date")] tb_tr_account_roles tb_tr_account_roles)
        {
            if (ModelState.IsValid)
            {
                tb_tr_account_roles.guid = Guid.NewGuid();
                db.tb_tr_account_roles.Add(tb_tr_account_roles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.account_guid = new SelectList(db.tb_m_accounts, "guid", "password", tb_tr_account_roles.account_guid);
            ViewBag.role_guid = new SelectList(db.tb_m_roles, "guid", "name", tb_tr_account_roles.role_guid);
            return View(tb_tr_account_roles);
        }

        // GET: AccountRole/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_tr_account_roles tb_tr_account_roles = db.tb_tr_account_roles.Find(id);
            if (tb_tr_account_roles == null)
            {
                return HttpNotFound();
            }
            ViewBag.account_guid = new SelectList(db.tb_m_accounts, "guid", "password", tb_tr_account_roles.account_guid);
            ViewBag.role_guid = new SelectList(db.tb_m_roles, "guid", "name", tb_tr_account_roles.role_guid);
            return View(tb_tr_account_roles);
        }

        // POST: AccountRole/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "guid,account_guid,role_guid,created_date,modified_date")] tb_tr_account_roles tb_tr_account_roles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_tr_account_roles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.account_guid = new SelectList(db.tb_m_accounts, "guid", "password", tb_tr_account_roles.account_guid);
            ViewBag.role_guid = new SelectList(db.tb_m_roles, "guid", "name", tb_tr_account_roles.role_guid);
            return View(tb_tr_account_roles);
        }

        // GET: AccountRole/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_tr_account_roles tb_tr_account_roles = db.tb_tr_account_roles.Find(id);
            if (tb_tr_account_roles == null)
            {
                return HttpNotFound();
            }
            return View(tb_tr_account_roles);
        }

        // POST: AccountRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            tb_tr_account_roles tb_tr_account_roles = db.tb_tr_account_roles.Find(id);
            db.tb_tr_account_roles.Remove(tb_tr_account_roles);
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

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
        /*private WebDbContextEntities db = new WebDbContextEntities();*/
        private readonly IAccountRoleRepository _db;

        public AccountRoleController(IAccountRoleRepository db)
        {
            _db = db;
        }

        // GET: AccountRole
        public ActionResult Index()
        {
            
            return View(_db.GetAll());
        }

        // GET: AccountRole/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_tr_account_roles tb_tr_account_roles = _db.GetByGuid(id);
            if (tb_tr_account_roles == null)
            {
                return HttpNotFound();
            }
            return View(tb_tr_account_roles);
        }

        // GET: AccountRole/Create
        public ActionResult Create()
        {
            
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
                tb_tr_account_roles.created_date = DateTime.Now;
                tb_tr_account_roles.modified_date = DateTime.Now;
                _db.Create(tb_tr_account_roles);
                return RedirectToAction("Index");
            }

           
            return View(tb_tr_account_roles);
        }

        // GET: AccountRole/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_tr_account_roles tb_tr_account_roles = _db.GetByGuid(id);
            if (tb_tr_account_roles == null)
            {
                return HttpNotFound();
            }
           
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
                _db.Update(tb_tr_account_roles);
                return RedirectToAction("Index");
            }
          
            return View(tb_tr_account_roles);
        }

        // GET: AccountRole/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_tr_account_roles tb_tr_account_roles = _db.GetByGuid(id);
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
            tb_tr_account_roles tb_tr_account_roles = _db.GetByGuid(id);
            _db.Delete(tb_tr_account_roles.guid);
            return RedirectToAction("Index");
        }

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}

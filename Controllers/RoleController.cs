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
    public class RoleController : Controller
    {
        /*private WebDbContextEntities db = new WebDbContextEntities();*/
        private readonly IRoleRepository _db;

        public RoleController(IRoleRepository db)
        {
            _db = db;
        }

        // GET: Role
        public ActionResult Index()
        {
            return View(_db.GetAll());
        }

        // GET: Role/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_roles tb_m_roles = _db.GetByGuid(id);
            if (tb_m_roles == null)
            {
                return HttpNotFound();
            }
            return View(tb_m_roles);
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "guid,name,created_date,modified_date")] tb_m_roles tb_m_roles)
        {
            if (ModelState.IsValid)
            {
                tb_m_roles.guid = Guid.NewGuid();
                tb_m_roles.created_date = DateTime.Now;
                tb_m_roles.modified_date = DateTime.Now;
                _db.Create(tb_m_roles);
                return RedirectToAction("Index");
            }

            return View(tb_m_roles);
        }

        // GET: Role/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_roles tb_m_roles = _db.GetByGuid(id);
            if (tb_m_roles == null)
            {
                return HttpNotFound();
            }
            return View(tb_m_roles);
        }

        // POST: Role/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "guid,name,created_date,modified_date")] tb_m_roles tb_m_roles)
        {
            if (ModelState.IsValid)
            {
                _db.Update(tb_m_roles);
                return RedirectToAction("Index");
            }
            return View(tb_m_roles);
        }

        // GET: Role/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_roles tb_m_roles = _db.GetByGuid(id);
            if (tb_m_roles == null)
            {
                return HttpNotFound();
            }
            return View(tb_m_roles);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            tb_m_roles tb_m_roles = _db.GetByGuid(id);
            _db.Delete(tb_m_roles.guid);
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

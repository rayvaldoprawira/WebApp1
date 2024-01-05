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
    public class ProjectController : Controller
    {
        private WebDbContextEntities db = new WebDbContextEntities();

        // GET: Project
        public ActionResult Index()
        {
            var tb_m_projects = db.tb_m_projects.Include(t => t.tb_m_vendors);
            return View(tb_m_projects.ToList());
        }

        // GET: Project/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_projects tb_m_projects = db.tb_m_projects.Find(id);
            if (tb_m_projects == null)
            {
                return HttpNotFound();
            }
            return View(tb_m_projects);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            ViewBag.vendor_guid = new SelectList(db.tb_m_vendors, "guid", "vendor_name");
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "guid,name,description,vendor_guid,created_date,modified_date")] tb_m_projects tb_m_projects)
        {
            if (ModelState.IsValid)
            {
                tb_m_projects.guid = Guid.NewGuid();
                db.tb_m_projects.Add(tb_m_projects);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.vendor_guid = new SelectList(db.tb_m_vendors, "guid", "vendor_name", tb_m_projects.vendor_guid);
            return View(tb_m_projects);
        }

        // GET: Project/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_projects tb_m_projects = db.tb_m_projects.Find(id);
            if (tb_m_projects == null)
            {
                return HttpNotFound();
            }
            ViewBag.vendor_guid = new SelectList(db.tb_m_vendors, "guid", "vendor_name", tb_m_projects.vendor_guid);
            return View(tb_m_projects);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "guid,name,description,vendor_guid,created_date,modified_date")] tb_m_projects tb_m_projects)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_m_projects).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.vendor_guid = new SelectList(db.tb_m_vendors, "guid", "vendor_name", tb_m_projects.vendor_guid);
            return View(tb_m_projects);
        }

        // GET: Project/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_projects tb_m_projects = db.tb_m_projects.Find(id);
            if (tb_m_projects == null)
            {
                return HttpNotFound();
            }
            return View(tb_m_projects);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            tb_m_projects tb_m_projects = db.tb_m_projects.Find(id);
            db.tb_m_projects.Remove(tb_m_projects);
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

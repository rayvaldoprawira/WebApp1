using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyApp.Core;
using MyApp.Interface;

namespace WebApp1.Controllers
{
    public class ProjectController : Controller
    {
        /*private WebDbContextEntities db = new WebDbContextEntities();*/
        private readonly IProjectRepository _db;

        public ProjectController(IProjectRepository db)
        {
            _db = db;
        }

        // GET: Project
        public ActionResult Index()
        {
            
            return View(_db.GetAll());
        }

        // GET: Project/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_projects tb_m_projects = _db.GetByGuid(id);
            if (tb_m_projects == null)
            {
                return HttpNotFound();
            }
            return View(tb_m_projects);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
           
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
                tb_m_projects.created_date = DateTime.Now;
                tb_m_projects.modified_date = DateTime.Now;
                _db.Create(tb_m_projects);
                return RedirectToAction("Index");
            }

            
            return View(tb_m_projects);
        }

        // GET: Project/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_projects tb_m_projects =_db.GetByGuid(id);
            if (tb_m_projects == null)
            {
                return HttpNotFound();
            }
           
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
                _db.Update(tb_m_projects);
                return RedirectToAction("Index");
            }
            
            return View(tb_m_projects);
        }

        // GET: Project/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_projects tb_m_projects = _db.GetByGuid(id);
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
            tb_m_projects tb_m_projects = _db.GetByGuid(id);
            _db.Delete(tb_m_projects.guid);
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

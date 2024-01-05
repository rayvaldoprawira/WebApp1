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
        /* private WebDbContextEntities db = new WebDbContextEntities();*/
        private readonly ICompanyRepository _db;

        public CompanyController(ICompanyRepository db)
        {
            _db = db;
        }

        // GET: tb_m_companies
        public ActionResult Index()
        {
            
            return View(_db.GetAll());
        }

        // GET: tb_m_companies/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_companies tb_m_companies = _db.GetByGuid(id);
            if (tb_m_companies == null)
            {
                return HttpNotFound();
            }
            return View(tb_m_companies);
        }

        // GET: tb_m_companies/Create
        public ActionResult Create()
        {
            
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
                tb_m_companies.created_date = DateTime.Now;
                tb_m_companies.modified_date = DateTime.Now;
                _db.Create(tb_m_companies);
                return RedirectToAction("Index");
            }

           
            return View(tb_m_companies);
        }

        // GET: tb_m_companies/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_companies tb_m_companies = _db.GetByGuid(id);
            if (tb_m_companies == null)
            {
                return HttpNotFound();
            }
           
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
               _db.Update(tb_m_companies);
                return RedirectToAction("Index");
            }
           
            return View(tb_m_companies);
        }

        // GET: tb_m_companies/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_companies tb_m_companies = _db.GetByGuid(id);
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
            tb_m_companies tb_m_companies = _db.GetByGuid(id);
            _db.Delete(tb_m_companies.guid);
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

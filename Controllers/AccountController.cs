using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyApp.Core;
using MyApp.Core.Dto.Accounts;

namespace WebApp1.Controllers
{
    public class AccountController : Controller
    {
        /*private WebDbContextEntities db = new WebDbContextEntities();*/
        private readonly IAccountRepository _db;

        public AccountController(IAccountRepository db)
        {
            _db = db;
        }

        // GET: Account
        public ActionResult Index()
        {
            return View(_db.GetAll());
        }

        // GET: Account/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_accounts tb_m_accounts = _db.GetByGuid(id);
            if (tb_m_accounts == null)
            {
                return HttpNotFound();
            }
            return View(tb_m_accounts);
        }

        // GET: Account/Create
        public ActionResult Create()
        {
           
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
                tb_m_accounts.created_date = DateTime.Now;
                tb_m_accounts.modified_date = DateTime.Now;
                _db.Create(tb_m_accounts);
                return RedirectToAction("Index");
            }

            
            return View(tb_m_accounts);
        }

        // GET: Account/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_accounts tb_m_accounts = _db.GetByGuid(id);
            if (tb_m_accounts == null)
            {
                return HttpNotFound();
            }
            
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
                _db.Update(tb_m_accounts);
                return RedirectToAction("Index");
            }
           
            return View(tb_m_accounts);
        }

        // GET: Account/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_m_accounts tb_m_accounts = _db.GetByGuid(id);
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
            tb_m_accounts tb_m_accounts = _db.GetByGuid(id);
            _db.Delete(tb_m_accounts.guid);
            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(AccountDtoRegister registerDto)
        {
            if (ModelState.IsValid)
            {
                if (_db.Register(registerDto))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed. Please try again.");
                }
            }

            return View(registerDto);
        }

        /*  protected override void Dispose(bool disposing)
          {
              if (disposing)
              {
                  db.Dispose();
              }
              base.Dispose(disposing);
          }*/
    }
}

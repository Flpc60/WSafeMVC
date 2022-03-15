using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Helpers;
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly EmpresaContext _empresaContext;
        private readonly IConverterHelper _converterHelper;
        private readonly IComboHelper _comboHelper;
        private readonly IGestorHelper _gestorHelper;

        public AccountsController
            (
                EmpresaContext empresaContext,
                IConverterHelper converterHelper,
                IComboHelper comboHelper,
                IGestorHelper gestorHelper
            )
        {
            empresaContext = _empresaContext;
            converterHelper = _converterHelper;
            comboHelper = _comboHelper;
            gestorHelper = _gestorHelper;
        }


        // GET: Accounts
        public async Task<ActionResult> Index()
        {
            return View(await _empresaContext.Users.ToListAsync());
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string user, string password)
        {
            if (user == null || password == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var result = _empresaContext.Users.Where(u => u.Email == user.Trim() && u.Password == password.Trim()).FirstOrDefault();
                if (result == null)
                {
                    ViewBag.Error = "Usuario o contraseña inválidos";
                    return View();
                }

                Session["User"] = result;

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }



        // GET: Accounts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await _empresaContext.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Email,Password,Fecha,RoleID")] User user)
        {
            if (ModelState.IsValid)
            {
                _empresaContext.Users.Add(user);
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Accounts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await _empresaContext.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Email,Password,Fecha,RoleID")] User user)
        {
            if (ModelState.IsValid)
            {
                _empresaContext.Entry(user).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Accounts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await _empresaContext.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await _empresaContext.Users.FindAsync(id);
            _empresaContext.Users.Remove(user);
            await _empresaContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _empresaContext.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return null;
        }
    }
}
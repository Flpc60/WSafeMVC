using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Helpers;
using WSafe.Web.Filters;
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
            _empresaContext = empresaContext;
            _converterHelper = converterHelper;
            _comboHelper = comboHelper;
            _gestorHelper = gestorHelper;
        }
        [AuthorizeUser(operation: 1, component: 9)]
        public ActionResult Index()
        {
            var model = new LoginViewModel();
            return View();
        }
        public ActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (model.Name == null || model.Email == null || model.Password == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var message = "";
                var result = _empresaContext.Users.Where(u => u.Name == model.Name.Trim() && u.Email == model.Email.Trim() && u.Password == model.Password.Trim()).FirstOrDefault();
                if (result == null)
                {
                    message = "Usuario o contraseña inválidos";
                    ViewBag.mensaje = message;
                    return View(model);
                }
                Session["User"] = result;
                Session["userName"] = result.Name;
                Session["roleID"] = result.RoleID;
                ViewBag.userName = Session["userName"].ToString().Trim();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
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
        [AuthorizeUser(operation: 2, component: 9)]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Email,Password,RoleID")] User model)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _empresaContext.Users.Where(u => u.Name == model.Name.Trim() && u.Email == model.Email.Trim() && u.Password == model.Password.Trim()).FirstOrDefault();
                    if (result == null)
                    {
                        _empresaContext.Users.Add(model);
                        await _empresaContext.SaveChangesAsync();
                        message = "Usuario registrado";
                    }
                    else
                    {
                        message = "El Usuario ya esta registrado";
                    }
                }
                else
                {
                    message = "El usuario NO pudo ser registrado";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            ViewBag.mensaje = message;
            return RedirectToAction("Login");
        }

        [AuthorizeUser(operation: 3, component: 9)]
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
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Email,Password,RoleID")] User user)
        {
            if (ModelState.IsValid)
            {
                _empresaContext.Entry(user).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }
        [AuthorizeUser(operation: 4, component: 9)]
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
            Session.Abandon();
            FormsAuthentication.SignOut();
            return null;
        }
    }
}
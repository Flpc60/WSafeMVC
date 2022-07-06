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
        [AuthorizeUser(operation: 1, component: 6)]
        public ActionResult Index()
        {
            var userList = _empresaContext.Users.ToList();
            var model = _converterHelper.ToUsersVM(userList);
            return View(model);
        }
        public ActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult LoginUser([Bind(Include = "ID,Name,Email,Password,RoleID")] LoginViewModel model)
        {
            var message = "";
            if (model.Name == null || model.Email == null || model.Password == null)
            {
                message = "El Usuario, Email, o la contraseña son inválidos !!";
                return Json(new { result = "", url = Url.Action("Index", "Home"), mensaj = message }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var result = _empresaContext.Users.Where(u => u.Name == model.Name.Trim() && u.Email == model.Email.Trim() && u.Password == model.Password.Trim()).FirstOrDefault();
                if (result == null || result.RoleID == 0)
                {
                    message = "El Usuario o la contraseña son inválidos, o no tiene un rol asignado en el sistema!!";
                    ViewBag.mensaje = message;
                    return Json(new { result = "", url = Url.Action("Index", "Home"), mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                Session["User"] = result;
                Session["userName"] = result.Name;
                Session["roleID"] = result.RoleID;
                return Json(new { result = "Redirect", url = Url.Action("Index", "Home"), mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                ViewBag.mensaje = ex.Message;
                return Json(new { result = "", url = Url.Action("Index", "Home"), mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }
        [AuthorizeUser(operation: 3, component: 6)]
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
            var model = _converterHelper.ToAuthorizationVM(user);
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(RoleUserVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = await _converterHelper.ToUserAsync(model);
                    _empresaContext.Entry(user).State = EntityState.Modified;
                    await _empresaContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
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
        [HttpPost]
        public async Task<ActionResult> CreateUser([Bind(Include = "ID,Name,Email,Password,RoleID")] User model)
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

            return Json(new { data = model, mensaj = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAllAuthorizations()
        {
            var authorize = _comboHelper.GetAllAuthorizations();
            var model = _converterHelper.ToRolOperationVM(authorize);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [AuthorizeUser(operation: 2, component: 6)]
        public ActionResult Create()
        {
            ViewBag.roles = _comboHelper.GetAllRoles();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateRoleOperation([Bind(Include = "ID,RoleID,Operation, Component")] RoleOperation model)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    message = "El registro ha sido ingresado exitosamente!!";
                    _empresaContext.RoleOperations.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    return Json(new { result = model, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "El registro NO ha sido ingresado exitosamente!!";
                return Json(new { result = model, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(new { result = model, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public async Task<ActionResult> DeleteAuthorization(int id)
        {
            var message = "";
            try
            {
                message = "El registro ha sido borrado correctamente!!";
                RoleOperation authorization = await _empresaContext.RoleOperations.FindAsync(id);
                _empresaContext.RoleOperations.Remove(authorization);
                await _empresaContext.SaveChangesAsync();
                return Json(new { result = true, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(new { result = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
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
        [HttpGet]
        public ActionResult GetAllRoles()
        {
            var roles = _comboHelper.GetNameRoles();
            return Json(roles, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRole(Role model)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    message = "El registro ha sido ingresado correctamente!!";
                    _empresaContext.Roles.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    return Json(new { result = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "El registro NO ha sido ingresado correctamente!!";
                return Json(new { result = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(new { result = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteRole(int id)
        {
            var message = "";
            try
            {
                Role role = await _empresaContext.Roles.FindAsync(id);

                // Validar retiro de rol
                var users = _empresaContext.Users.Where(t => t.RoleID == id).Count();
                if (users != 0)
                {
                    message = "Este rol NO se puede borrar por estar asignado  a un usuario!!";
                    return Json(new { result = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }

                message = "Este rol fué borrado correctamente!!";
                _empresaContext.Roles.Remove(role);
                await _empresaContext.SaveChangesAsync();
                return Json(new { result = true, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(new { result = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(int id)
        {

            var message = "";
            try
            {
                User user = await _empresaContext.Users.FindAsync(id);
                var role = (int)Session["roleID"];
                var roleName = _gestorHelper.GetRole(user.RoleID);

                // Validar retiro de usuario
                if (user.RoleID == role || roleName == "ADMIN")
                {
                    message = "Este usuario NO se pudo borrar por estar en el sistema, o tener el rol de administrador!!";
                    return Json(new { result = "Redirect", url = Url.Action("Index", "Accounts"), mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "El usuario fué borrado correctamente!!";
                _empresaContext.Users.Remove(user);
                await _empresaContext.SaveChangesAsync();
                return Json(new { result = "Redirect", url = Url.Action("Index", "Accounts"), mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(new { result = "Redirect", url = Url.Action("Index", "Accounts"), mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
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
        // Gestión de cuentas
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
                string password = GetSHA256(model.Password);
                var result = _empresaContext.Users.Where(u => u.Name == model.Name.Trim() && u.Email == model.Email.Trim() && u.Password == password.Trim()).FirstOrDefault();
                if (result == null || result.RoleID == 0)
                {
                    message = "El Usuario o la contraseña son inválidos, o no tiene un rol asignado en el sistema!!";
                    ViewBag.mensaje = message;
                    return Json(new { result = "", url = Url.Action("Index", "Home"), mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                var id = _empresaContext.Organizations.OrderByDescending(x => x.ID).First().ID;
                var empresa = _empresaContext.Organizations.FirstOrDefault(o => o.ID == id);

                if (empresa.ControlDate < DateTime.Now)
                {
                    message = "Favor enviar recibo de pago para activar el servicio !!";
                    ViewBag.mensaje = message;
                    return Json(new { result = "", url = Url.Action("Index", "Home"), mensaj = message }, JsonRequestBehavior.AllowGet);
                }

                var agropecuaria = "NO";
                if (empresa.Agropecuaria == true)
                {
                    agropecuaria = "SI";
                }

                Session["User"] = result;
                Session["userName"] = result.Name;
                Session["roleID"] = result.RoleID;
                Session["userID"] = result.ID;
                Session["organization"] = empresa.RazonSocial.Trim() + " NIT : " + empresa.NIT + "     Agropecuaria : " + agropecuaria;
                Session["numeroTrabajadores"] = empresa.NumeroTrabajadores;
                Session["turnoOperativo"] = empresa.TurnosOperativo;
                Session["year"] = empresa.Year;
                Session["riesgo"] = empresa.ClaseRiesgo;
                Session["responsable"] = empresa.ResponsableSGSST;
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
                    string password = GetSHA256(model.Password);
                    var result = _empresaContext.Users.Where(u => u.Name == model.Name.Trim() && u.Email == model.Email.Trim() && u.Password == password.Trim()).FirstOrDefault();
                    if (result == null)
                    {
                        model.Password = password;
                        _empresaContext.Users.Add(model);
                        await _empresaContext.SaveChangesAsync();
                        message = "El Usuario ha sido registrado";
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
            try
            {
                var message = "";
                if (ModelState.IsValid)
                {
                    message = "El registro ha sido ingresado exitosamente!!";
                    _empresaContext.RoleOperations.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    return Json(new { result = model, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    message = "El registro NO ha sido ingresado exitosamente!!";
                    return Json(new { result = model, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Accounts", "Index"));
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
        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}
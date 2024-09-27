using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Helpers;
using WSafe.Domain.Models;
using WSafe.Web.Filters;

namespace WSafe.Web.Controllers
{
    public class AccountsController : Controller
    {
        // Gestión de cuentas de usuarios
        private int _clientID;
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
            _clientID = (int)Session["clientID"];
            var model = _converterHelper.ToUsersVM(_clientID);
            var orgList = (from o in _empresaContext.Organizations
                           where (o.ClientID == _clientID)
                           select new SelectListItem
                           {
                               Text = o.RazonSocial.Trim() + " - " + o.NIT.Trim(),
                               Value = o.ID.ToString()
                           })
                .ToList();

            var orgUsers = (from u in _empresaContext.Users
                            where (u.ClientID == _clientID)
                            orderby u.Name
                            select new SelectListItem
                            {
                                Text = u.Name.ToUpper(),
                                Value = u.ID.ToString()
                            })
                .ToList();
            ViewBag.userList = orgUsers;
            ViewBag.orgList = orgList;
            ViewBag.organization = $"GESTIÓN DE USUARIOS: {Session["organization"].ToString().Trim()}";
            return View(model);
        }
        public ActionResult Login()
        {
            var model = new LoginViewModel();
            model.Clients = _comboHelper.GetClients();
            return View(model);
        }

        [HttpPost]
        public ActionResult LoginUser([Bind(Include = "ID,Name,Email,Password,RoleID,OrganizationID,ClientID")] LoginViewModel model)
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
                if (model.OrganizationID == 0)
                {
                    var userList = (from o in _empresaContext.Organizations
                                    where (o.ClientID == model.ClientID)
                                    select new
                                    {
                                        Text = o.RazonSocial.Trim() + " - " + o.NIT.Trim(),
                                        Value = o.ID
                                    })
                        .ToList();
                    message = "Seleccione una organización !!";
                    return Json(new { result = userList, url = Url.Action("Index", "Home"), mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                var result = _empresaContext.Users.Where(u => u.Name == model.Name.Trim() && u.Email == model.Email.Trim() && u.Password == password.Trim() && u.OrganizationID == model.OrganizationID).FirstOrDefault();

                var responsable = _empresaContext.Users.Where(u => u.ResponsableSgsst == true).FirstOrDefault();
                if (result == null || result.RoleID == 0)
                {
                    message = "El Usuario, la contraseña o la organización son inválidos, o no tiene un rol asignado en el sistema!!";
                    ViewBag.mensaje = message;
                    return Json(new { result = "", url = Url.Action("Index", "Home"), mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                var empresa = _empresaContext.Organizations.Find(model.OrganizationID);

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

                var roleUser = _gestorHelper.GetRole(result.RoleID);
                var classRisk = _gestorHelper.GetRiskClass((int)empresa.ClaseRiesgo);

                // Capturar firma electrónica
                string signatureBase64;

                if (responsable.Firma != null)
                {
                    signatureBase64 = Convert.ToBase64String(responsable.Firma);
                }
                else
                {
                    signatureBase64 = Convert.ToBase64String(responsable.FirmaElectronica);
                }

                Session["signatureResponsable"] = signatureBase64;
                Session["UserRole"] = roleUser;
                Session["User"] = result;
                Session["userName"] = result.Name;
                Session["roleID"] = result.RoleID;
                Session["userID"] = result.ID;
                Session["organization"] = empresa.RazonSocial.Trim() + " NIT : " + empresa.NIT;
                Session["numeroTrabajadores"] = empresa.NumeroTrabajadores;
                Session["turnoOperativo"] = empresa.TurnosOperativo;
                Session["year"] = empresa.Year.ToString();
                Session["riesgo"] = classRisk;
                Session["responsable"] = empresa.ResponsableSGSST;
                Session["clientID"] = empresa.ClientID;
                Session["orgID"] = empresa.ID;
                Session["path"] = $"~/ORG{empresa.ID}/SG-SST/{empresa.Year}";
                Session["logo"] = $"~/Images/logo{empresa.ID}.jpg";
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
                //TODO
                model.OrganizationID = (int)Session["orgID"];
                model.Roles = _comboHelper.GetAllRoles();

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
        public async Task<ActionResult> CreateUser([Bind(Include = "ID,Name,Email,Password,RoleID,OrganizationID,ClientID,FirmaElectronica,ResponsableSgsst")] User model)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrWhiteSpace(model.Password))
                    {
                        message = "La contraseña no puede estar vacía.";
                        return Json(new { data = model, mensaj = message }, JsonRequestBehavior.AllowGet);
                    }

                    string password = GetSHA256(model.Password);
                    var result = _empresaContext.Users
                                .FirstOrDefault(u => u.Name == model.Name.Trim() &&
                                                     u.Email == model.Email.Trim() &&
                                                     u.Password == password);

                    if (result == null)
                    {
                        model.ClientID = model.ClientID == 0 ? 1 : model.ClientID;
                        model.Password = password;
                        model.RegisterDate = DateTime.Now;
                        _empresaContext.Users.Add(model);
                        await _empresaContext.SaveChangesAsync();
                        message = "El Usuario se ha registrado correctamente !!";
                    }
                    else
                    {
                        model.Password = password;
                        model.RegisterDate = DateTime.Now;
                        User user = _empresaContext.Users
                                    .FirstOrDefault(u => u.Name == model.Name.Trim() &&
                                                         u.Email == model.Email.Trim() &&
                                                         u.Password == model.Password);
                        user.ResponsableSgsst = model.ResponsableSgsst;
                        user.FirmaElectronica = model.FirmaElectronica;
                        user.Firma = null;

                        _empresaContext.Entry(user).State = EntityState.Modified;
                        await _empresaContext.SaveChangesAsync();
                        message = "El Usuario se ha actualizado correctamente !!";
                    }
                }
                else
                {
                    message = "El usuario NO se pudo registrar !!";
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                message = $"Error: {errorMessage}";
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
                    message = "El registro ha sido ingresado exitosamente !!";
                    _empresaContext.RoleOperations.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    return Json(new { result = model, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    message = "El registro NO ha sido ingresado exitosamente !!";
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
                RoleOperation authorization = await _empresaContext.RoleOperations.FindAsync(id);
                var roleName = _gestorHelper.GetRole(authorization.RoleID);
                if (roleName != "ADMIN")
                {
                    message = "El registro ha sido borrado correctamente !!";
                    _empresaContext.RoleOperations.Remove(authorization);
                    await _empresaContext.SaveChangesAsync();
                    return Json(new { result = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }

                message = "El registro NO ha sido borrado correctamente !!";
                return Json(new { result = false, mensaj = message }, JsonRequestBehavior.AllowGet);
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
            return Json(true, JsonRequestBehavior.AllowGet);
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
                    message = $"El rol {model.Name.ToUpper().Trim()} ha sido ingresado correctamente !!";
                    _empresaContext.Roles.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    return Json(new { result = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = $"El rol {model.Name.ToUpper().Trim()} NO ha sido ingresado correctamente !!";
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
                var roleOperations = _empresaContext.RoleOperations.Where(ro => ro.RoleID == id).Count();
                var users = _empresaContext.Users.Where(t => t.RoleID == id).Count();
                if (users != 0 || roleOperations != 0)
                {
                    message = $"El rol {role.Name.ToUpper().Trim()} NO se pudo borrar por estar asignado a un usuario o perfil de usuario !!";
                    return Json(new { result = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }

                message = $"El rol {role.Name.ToUpper().Trim()} fué borrado correctamente !!";
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
                var roleName = "";
                if (user.RoleID != 0)
                {
                    roleName = _gestorHelper.GetRole(user.RoleID);
                }

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
        [HttpPost]
        public async Task<ActionResult> CreateAuthorization(int userID, int organizationID)
        {
            var message = "";
            try
            {
                User user = _empresaContext.Users.Find(userID);
                user.OrganizationID = organizationID;
                _empresaContext.Users.Add(user);
                await _empresaContext.SaveChangesAsync();
                message = "El usuario se ha autorizado correctamente !!";
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(new { result = "", url = Url.Action("Index", "Accounts"), mensaj = message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = "Redirect", url = Url.Action("Index", "Accounts"), mensaj = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadSignature(string Name, string Email, string Password, bool responsable, HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se ha seleccionado ningún archivo.");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Formato de archivo no permitido. Solo JPG y PNG son aceptados.");
            }

            // Convertir la imagen a byte[]
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                file.InputStream.CopyTo(memoryStream); // Usar InputStream para leer el archivo
                fileBytes = memoryStream.ToArray();
            }

            // Guardar la firma en la base de datos
            string password = GetSHA256(Password);
            var user = _empresaContext.Users
                        .FirstOrDefault(u => u.Name == Name.Trim() &&
                                             u.Email == Email.Trim() &&
                                             u.Password == password);

            if (user != null)
            {
                user.Firma = fileBytes;
                user.FirmaElectronica = null;
                user.ResponsableSgsst = responsable;
                _empresaContext.SaveChanges();
                return Json(new { message = "Firma subida correctamente." });
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Usuario no encontrado.");
        }
    }
}
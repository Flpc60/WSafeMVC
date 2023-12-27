using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Helpers;
using WSafe.Web.Filters;
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    public class MovimientosController : Controller
    {
        // Gestionar explorador carpetas SG-SST para auditorías internas
        private int _clientID;
        private int _orgID;
        private string _year;
        private string _path;
        private int _operation;
        private int _roleID;
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IChartHelper _chartHelper;
        public MovimientosController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper, IChartHelper chartHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _chartHelper = chartHelper;
        }

        // GET: Movimientos
        [AuthorizeUser(operation: 1, component: 2)]
        public ActionResult Index()
        {
            ViewBag.Users = _comboHelper.GetComboUsers();
            return View();
        }

        // GET: Movimientos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimient movimientVM = await _empresaContext.Movimientos.FindAsync(id);
            if (movimientVM == null)
            {
                return HttpNotFound();
            }
            return View(movimientVM);
        }

        // GET: Movimientos/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMovimient(string Descripcion, int NormaID, HttpPostedFileBase fileLoad)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                _path = (string)Session["path"];
                var message = "";
                var organizatión = _empresaContext.Organizations.Find(_orgID);
                var year = _year;
                var normaID = NormaID.ToString();
                var userID = (int)Session["userID"];
                var descript = Descripcion.ToString();
                var norma = _empresaContext.Normas.Find(NormaID);
                var cycle = norma.Ciclo.ToString();
                var ruta = norma.Ciclo.ToString();

                switch (cycle)
                {
                    case "P":
                        ruta = "1. PLANEAR";
                        break;

                    case "H":
                        ruta = "2. HACER";
                        break;
                    case "V":
                        ruta = "3. VERIFICAR";
                        break;
                    case "A":
                        ruta = "4. ACTUAR";
                        break;
                }

                var item = norma.Item.ToString();
                var fullPath = $"{_path}/{ruta}/{item}/";
                var path = Server.MapPath(fullPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                fileLoad.SaveAs(path + Path.GetFileName(fileLoad.FileName));
                var fullName = fileLoad.FileName;
                var type = Path.GetExtension(fileLoad.FileName).ToUpper();

                // Crear movimiento de documentos
                Movimient model = new Movimient()
                {
                    ID = 0,
                    OrganizationID = _orgID,
                    NormaID = NormaID,
                    UserID = userID,
                    Descripcion = descript,
                    Document = fullName,
                    Year = year,
                    Item = item,
                    Ciclo = cycle,
                    Type = type,
                    Path = path,
                    ClientID = _clientID
                };
                _empresaContext.Movimientos.Add(model);

                // Generar trazabilidad 
                var model1 = _converterHelper.Traceability(norma.ID, year, _orgID, fullName);
                if (model1 != null)
                {
                    _empresaContext.SigueAnnualPlans.Add(model1);
                }
                _empresaContext.SaveChanges();

                var idMovimient = _empresaContext.Movimientos.OrderByDescending(x => x.ID).First().ID;
                message = "El archivo ha sido subido correctamente !!";
                return Json(new { data = idMovimient, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Movimientos", "Index"));
            }
        }

        [HttpGet]
        public async Task<ActionResult> OpenFile(int? id)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                _path = (string)Session["path"];
                Movimient model = await _empresaContext.Movimientos.FindAsync(id);
                var message = "";
                if (model == null)
                {
                    message = "El archivo NO ha sido abierto correctamente !!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                var cycle = model.Ciclo.ToString();
                var ruta = model.Ciclo.ToString();

                switch (cycle)
                {
                    case "P":
                        ruta = "1. PLANEAR";
                        break;

                    case "H":
                        ruta = "2. HACER";
                        break;
                    case "V":
                        ruta = "3. VERIFICAR";
                        break;
                    case "A":
                        ruta = "4. ACTUAR";
                        break;
                }

                var year = model.Year.ToString();
                var item = model.Item.ToString();
                var fullFilePath = $"{_path}/{ruta}/{item}/{model.Document}";
                var path = Server.MapPath(fullFilePath);
                using (Process myProcess = new Process())
                {

                    myProcess.StartInfo.UseShellExecute = true;
                    myProcess.StartInfo.FileName = path;
                    myProcess.StartInfo.CreateNoWindow = false;
                    Process.Start(path);
                }
                return Json(new { data = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Movimientos", "Index"));
            }
        }

        // POST: Movimientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,OrganizationID,Ciclo,Item,Name,NormaID,Descripcion,Document,Year")] MovimientVM movimientVM)
        {
            if (ModelState.IsValid)
            {
                _empresaContext.Entry(movimientVM).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(movimientVM);
        }

        // GET: Movimientos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimientVM movimientVM = await _empresaContext.MovimientVMs.FindAsync(id);
            if (movimientVM == null)
            {
                return HttpNotFound();
            }
            return View(movimientVM);
        }

        // POST: Movimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MovimientVM movimientVM = await _empresaContext.MovimientVMs.FindAsync(id);
            _empresaContext.MovimientVMs.Remove(movimientVM);
            await _empresaContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult GetNormas(string ciclo)
        {
            if (ciclo != null)
            {
                var result =
                    from n in _empresaContext.Normas
                    where n.Ciclo == ciclo
                    select new
                    {
                        Value = n.ID,
                        Text = n.Item + " " + n.Name
                    };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMovimientos(string ciclo, int item, string year)
        {
            if (ciclo != null)
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                var _year = year;
                _path = (string)Session["path"];
                var data = _empresaContext.Movimientos
                    .Where(m => m.Ciclo == ciclo && m.NormaID == item && m.Year == _year && m.OrganizationID == _orgID)
                    .OrderBy(m => m.Item)
                    .ToList();
                var result = _converterHelper.ToListMovimientos(data);
                return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePDF(int id)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                _path = (string)Session["path"];
                var message = "";
                Movimient model = await _empresaContext.Movimientos.FindAsync(id);
                if (model == null)
                {
                    message = "El archivo NO ha sido abierto correctamente !!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                var cycle = model.Ciclo.ToString();
                var ruta = model.Ciclo.ToString();

                switch (cycle)
                {
                    case "P":
                        ruta = "1. PLANEAR";
                        break;

                    case "H":
                        ruta = "2. HACER";
                        break;
                    case "V":
                        ruta = "3. VERIFICAR";
                        break;
                    case "A":
                        ruta = "4. ACTUAR";
                        break;
                }

                var year = model.Year.ToString();
                var item = model.Item.ToString();
                var fullFilePath = $"{_path}/{ruta}/{item}/{model.Document}";
                var filePath = $"{_path}/{ruta}/{item}/";
                var path = Server.MapPath(fullFilePath);
                var directoryPath = Server.MapPath(filePath);
                var type = model.Type.ToLower();
                var pathPdf = path.Replace(type, ".pdf");
                // Crear PDF
                //DocumentCore docFile = DocumentCore.Load(path);
                //DocumentCore docFile = DocumentCore.Load(path);
                //docFile.Save(pathPdf);
                var docFile = new Aspose.Words.Document(path);
                docFile.Save(pathPdf);

                // Crear movimiento de documentos
                var docum = model.Document;
                var filePdf = docum.Replace(type, ".pdf");
                type = ".PDF";

                Movimient data = new Movimient()
                {
                    ID = 0,
                    OrganizationID = model.OrganizationID,
                    NormaID = model.NormaID,
                    Descripcion = model.Descripcion,
                    Document = filePdf,
                    Year = model.Year,
                    Item = model.Item,
                    Ciclo = model.Ciclo,
                    Type = type,
                    Path = directoryPath,
                    ClientID = _clientID
                };

                _empresaContext.Movimientos.Add(data);
                await _empresaContext.SaveChangesAsync();
                message = "El archivo ha sido creado correctamente !!";
                return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Movimientos", "Index"));
            }
        }
        [HttpPost]
        public ActionResult Download(int id)
        {
            try
            {
                Movimient model = _empresaContext.Movimientos.Find(id);
                return Json(new { url = model.Path, name = model.Document }, JsonRequestBehavior.AllowGet);
            }
            catch (Win32Exception w)
            {
                return Json(new { data = false, mensaj = w.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteFile(int id)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                _path = (string)Session["path"];
                var message = "";
                Movimient model = await _empresaContext.Movimientos.FindAsync(id);
                if (model == null)
                {
                    message = "El archivo NO se ha abierto correctamente !!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }

                var cycle = model.Ciclo.ToString();
                var ruta = model.Ciclo.ToString();
                switch (cycle)
                {
                    case "P":
                        ruta = "1. PLANEAR";
                        break;

                    case "H":
                        ruta = "2. HACER";
                        break;
                    case "V":
                        ruta = "3. VERIFICAR";
                        break;
                    case "A":
                        ruta = "4. ACTUAR";
                        break;
                }

                var year = model.Year.ToString();
                var item = model.Item.ToString();
                var fullFilePath = $"{_path}/{ruta}/{item}/";
                string fullPath = Server.MapPath(fullFilePath);
                string fileName = model.Document;
                string fileLocation = Path.Combine(fullPath, fileName);
                FileInfo file = new FileInfo(fileLocation);
                file.Delete();
                _empresaContext.Movimientos.Remove(model);
                await _empresaContext.SaveChangesAsync();
                message = "El archivo se ha eliminado correctamente !!";
                return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Win32Exception w)
            {
                return Json(new { data = false, mensaj = w.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public async Task<ActionResult> SendMail(int id, int destino1, int destino2, int destino3, string sendAsunto, string sendMessage)
        {
            var message = "";
            try
            {
                Movimient model = await _empresaContext.Movimientos.FindAsync(id);
                if (model == null)
                {
                    message = "El archivo NO ha sido abierto correctamente !!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                var cycle = model.Ciclo.ToString();
                var ruta = model.Ciclo.ToString();

                switch (cycle)
                {
                    case "P":
                        ruta = "1. PLANEAR/";
                        break;

                    case "H":
                        ruta = "2. HACER/";
                        break;
                    case "V":
                        ruta = "3. VERIFICAR/";
                        break;
                    case "A":
                        ruta = "4. ACTUAR/";
                        break;
                }

                var year = model.Year.ToString();
                var item = model.Item.ToString();
                var fullFilePath = $"{_path}/{ruta}/{item}/";
                string fullPath = Server.MapPath(fullFilePath);
                string fileName = model.Document;

                // Enviar Email
                string fileLocation = Path.Combine(fullPath, fileName);
                var userID = (int)Session["userID"];
                string emailOrigen = _empresaContext.Users.Find(userID).Email;
                string emailDestino1 = _empresaContext.Users.Find(destino1).Email;
                string emailDestino2 = _empresaContext.Users.Find(destino2).Email;
                string emailDestino3 = _empresaContext.Users.Find(destino3).Email;
                string contraseña = "ryexorlfkqdqpvls";
                string asunto = sendAsunto;
                string contenido = sendMessage;
                MailMessage oMailMessage = new MailMessage();
                oMailMessage.From = new MailAddress(emailOrigen);
                oMailMessage.Subject = asunto;
                oMailMessage.To.Add(new MailAddress(emailDestino1));
                oMailMessage.To.Add(new MailAddress(emailDestino2));
                oMailMessage.To.Add(new MailAddress(emailDestino3));
                oMailMessage.Body = contenido;
                oMailMessage.IsBodyHtml = true;
                oMailMessage.Attachments.Add(new Attachment(fileLocation));
                SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
                oSmtpClient.EnableSsl = true;
                oSmtpClient.UseDefaultCredentials = false;
                oSmtpClient.Port = 587;
                oSmtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, contraseña);
                oSmtpClient.Send(oMailMessage);
                oSmtpClient.Dispose();
                message = "El archivo se ha enviado correctamente !!";
                return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Win32Exception w)
            {
                return Json(new { data = false, mensaj = w.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
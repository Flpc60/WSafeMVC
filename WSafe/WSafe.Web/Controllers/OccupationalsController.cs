using Rotativa;
using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Helpers;
using WSafe.Domain.Models;
using WSafe.Domain.Repositories.Implements;
using WSafe.Domain.Services.Implements;

namespace WSafe.Web.Controllers
{
    public class OccupationalsController : Controller
    {
        //  Evaluaciones médicas  en el SG-SST
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
        private readonly IGestorHelper _gestorHelper;
        public OccupationalsController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper, IChartHelper chartHelper, IGestorHelper gestorHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _chartHelper = chartHelper;
            _gestorHelper = gestorHelper;
        }

        // GET: Occupationals
        public async Task<ActionResult> Index()
        {
            try
            {
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                var list = await _empresaContext.Occupationals
                    .Where(o => o.OrganizationID == _orgID && o.ExaminationDate.Year.ToString() == _year)
                    .OrderByDescending(o => o.ExaminationDate)
                    .Include(s => s.SigueOccupational)
                    .ToListAsync();
                var model = _converterHelper.ToMedicalRecomendationVM(list);
                ViewBag.organization = $"GESTIÓN DE EXÁMENES MÉDICOS: {Session["organization"].ToString().Trim()}";

                return View(model.ToList());
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Occupationals", "Index"));
            }
        }
        public ActionResult Create()
        {
            _orgID = (int)Session["orgID"];
            var model = _converterHelper.ToCreateOccupationalVM(_orgID);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ExaminationDate,TrabajadorID,Workers, Talla,Peso,ExaminationType,Recomendations,MedicalRecomendation,SigueOccupational,OrganizationID,ClientID,UserID, FileName")] CreateOccupationalVM model, HttpPostedFileBase fileLoad)
        {
            try
            {

                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                _path = (string)Session["path"];
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
                model.UserID = (int)Session["userID"];
                model.Workers = _comboHelper.GetWorkersFull(_orgID);
                if (fileLoad != null)
                {
                    var url = $"{_path}/2. HACER/3.1.4/{fileLoad.FileName}";
                    model.FileName = url.Substring(1);
                }
                if (ModelState.IsValid)
                {
                    var organization = _empresaContext.Organizations.Find(_orgID);
                    var normaID = organization.StandardOccupational;
                    var item = _empresaContext.Normas.Find(normaID).Item;
                    var fullPath = $"{_path}/2. HACER/{item}/";
                    var path = Server.MapPath(fullPath);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    fileLoad.SaveAs(path + Path.GetFileName(fileLoad.FileName));
                    var fullName = fileLoad.FileName;
                    var type = Path.GetExtension(fileLoad.FileName).ToUpper();

                    //Generar archivo de movimiento
                    var descript = "Evaluaciones médicas ocupacionales";
                    var userID = (int)Session["userID"];
                    Movimient movimient = new Movimient()
                    {
                        ID = 0,
                        OrganizationID = _orgID,
                        NormaID = normaID,
                        UserID = userID,
                        Descripcion = descript,
                        Document = fileLoad.FileName,
                        Year = _year,
                        Item = item,
                        Ciclo = "H",
                        Type = type,
                        Path = path,
                        ClientID = _clientID
                    };

                    _empresaContext.Movimientos.Add(movimient);
                    _empresaContext.SaveChanges();

                    var consulta = new OccupationalService(new OccupationalRepository(_empresaContext));
                    var occupational = _converterHelper.ToOccupationalAsync(model, true);
                    var saved = await consulta.Insert(occupational);
                    if (saved != null)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Occupationals", "Index"));
            }

            ViewBag.message = "Faltan campos por diligenciar del formulario !!";

            return View(model);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                Occupational occupational = await _empresaContext.Occupationals.FindAsync(id);
                var model = _converterHelper.ToUpdateOccupationalVM(occupational, _orgID);
                ViewBag.guardar = true;

                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Occupationals", "Index"));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID, ExaminationDate, TrabajadorID, Workers, Talla, Peso, ExaminationType, Recomendations, MedicalRecomendation, SigueOccupational, OrganizationID, ClientID, UserID, FileName")] CreateOccupationalVM model, HttpPostedFileBase fileLoad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Occupational occupational = _converterHelper.ToOccupationalAsync(model, false);
                    _empresaContext.Entry(occupational).State = EntityState.Modified;
                    await _empresaContext.SaveChangesAsync();
                    _orgID = (int)Session["orgID"];
                    model.Workers = _comboHelper.GetWorkersFull(_orgID);
                    ViewBag.guardar = false;
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Occupationals", "Index"));
            }

            _orgID = model.OrganizationID;
            model.Workers = _comboHelper.GetWorkersFull(_orgID);
            ViewBag.message = "Faltan campos por diligenciar del formulario !!";
            ViewBag.guardar = true;

            return View(model);
        }

        [HttpGet]
        public ActionResult GetSigueOccupationals(int id)
        {
            try
            {
                var list =
                    from s in _empresaContext.SigueOccupationals
                    where (s.OccupationalID == id)
                    orderby s.SigueDate
                    select new
                    {
                        ID = s.ID,
                        OccupationalID = s.OccupationalID,
                        DateSigue = s.SigueDate,
                        Resultado = s.Resultado,
                        Recomendations = s.Recomendations,
                        TrabajadorID = s.TrabajadorID
                    };

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch
            {

                var message = "La conslta NO se ha realizado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> UpdateSigueOccupational(int id)
        {
            _orgID = (int)Session["orgID"];
            _year = (string)Session["year"];

            SigueOccupational model = await _empresaContext.SigueOccupationals.FindAsync(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateSigueOccupational(SigueOccupational model)
        {
            var message = "";
            try
            {
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
                model.UserID = (int)Session["userID"];

                _empresaContext.Entry(model).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
                message = "La actualización se ha realizado exitosamente !!";
                return Json(new { data = model.OccupationalID, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "La actualización NO se ha realizado exitosamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> DeleteSigueOccupational(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _orgID = (int)Session["orgID"];
            _year = (string)Session["year"];

            SigueOccupational model = await _empresaContext.SigueOccupationals.FindAsync(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteSigueOccupational(int id)
        {
            try
            {
                SigueOccupational sique = await _empresaContext.SigueOccupationals.FindAsync(id);
                _empresaContext.SigueOccupationals.Remove(sique);
                await _empresaContext.SaveChangesAsync();
                var message = "El registro se ha borrado exitosamente !!";
                return Json(new { data = sique.OccupationalID, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                var message = "El registro NO se ha borrado exitosamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateSigueOccupational(SigueOccupational model)
        {
            if (model.OccupationalID == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var message = "";
            try
            {
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
                model.UserID = (int)Session["userID"];

                if (ModelState.IsValid)
                {
                    _empresaContext.SigueOccupationals.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    message = "El registro ha sido ingresado correctamente !!";
                    return Json(new { data = model, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "El registro NO ha sido ingresado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "El registro NO ha sido ingresado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteOccupational(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                _path = (string)Session["path"];

                var message = "";
                Occupational result = await _empresaContext.Occupationals
                    .Include(s => s.SigueOccupational)
                    .FirstOrDefaultAsync(o => o.ID == id);

                if (result != null)
                {
                    var sigue = result.SigueOccupational
                        .Where(s => s.OccupationalID == id)
                        .Count();
                    if (sigue != 0)
                    {
                        message = "Esta evaluación ocupacional tiene seguimientos !!";
                        return Json(new { data = false, error = message }, JsonRequestBehavior.AllowGet);
                    }
                    var model = _converterHelper.ToUpdateOccupationalVM(result, _orgID);
                    return Json(new { data = model, error = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    message = "No se encontró la actividad con el ID proporcionado.";
                    return Json(new { data = false, error = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "AnnualPlans", "Index"));
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteOccupational(int id)
        {
            var consulta = new OccupationalService(new OccupationalRepository(_empresaContext));
            try
            {
                await consulta.Delete(id);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Occupationals", "Delete"));
            }

            return Json(new { data = true, message = "El registro ha sido eliminado exitosamente" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> OccupationalsPdf()
        {
            try
            {
                // Configuración nombre archivo pdf
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                _path = (string)Session["path"];
                var organization = _empresaContext.Organizations.Find(_orgID);
                var year = _year;
                var item = _empresaContext.Normas.Find(organization.StandardOccupational).Item;
                var fullPath = $"{_path}/2. HACER/{item}/";
                var path = Server.MapPath(fullPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Random random = new Random();
                var filename = "Matriz de evaluaciones médicas ocupacionales" + random.Next(1, 100) + ".Pdf";
                var filePathName = path + filename;
                var list = await _empresaContext.Occupationals
                    .Where(o => o.OrganizationID == _orgID && o.ExaminationDate.Year.ToString() == _year)
                    .OrderByDescending(o => o.ExaminationDate)
                    .Include(s => s.SigueOccupational)
                    .ToListAsync();
                var model = _converterHelper.ToMedicalRecomendationVM(list);
                var document = _empresaContext.Documents.FirstOrDefault(d => d.ID == 15);
                ViewBag.formato = document.Formato;
                ViewBag.estandar = document.Estandar;
                ViewBag.titulo = document.Titulo;
                ViewBag.version = document.Version;
                ViewBag.fecha = DateTime.Now;
                var report = new ViewAsPdf("CreateOccupational");
                report.Model = model;
                report.FileName = filePathName;
                report.PageSize = Rotativa.Options.Size.A4;
                report.PageOrientation = Rotativa.Options.Orientation.Landscape;
                report.PageWidth = 399;
                report.PageHeight = 399;
                report.SaveOnServerPath = filePathName;

                //Generar archivo de movimiento
                var fullName = filename;
                var type = Path.GetExtension(filename).ToUpper();
                var descript = "MATRIZ SEGUIMIENTO EVALUACIONES MÉDICAS OCUPACIONALES";
                var userID = (int)Session["userID"];
                Movimient movimient = new Movimient()
                {
                    ID = 0,
                    OrganizationID = _orgID,
                    NormaID = organization.StandardOccupational,
                    UserID = userID,
                    Descripcion = descript,
                    Document = fullName,
                    Year = year,
                    Item = item,
                    Ciclo = "H",
                    Type = type,
                    Path = path,
                    ClientID = _clientID
                };
                _empresaContext.Movimientos.Add(movimient);
                _empresaContext.SaveChanges();
                return report;
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Occupationals", "Index"));
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
    }
}

using Rotativa;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Helpers;
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    public class WorkersController : Controller
    {
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
        public WorkersController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper, IChartHelper chartHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _chartHelper = chartHelper;
        }

        // GET: Workers
        public async Task<ActionResult> Index()
        {
            try
            {
                _orgID = (int)Session["orgID"];
                var model = await _empresaContext.Trabajadores
                    .Where(t => t.OrganizationID == _orgID)
                    .OrderBy(t => t.PrimerApellido)
                    .ThenBy(t => t.SegundoApellido)
                    .ThenBy(t => t.Nombres)
                    .ToListAsync();
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Workers", "Index"));
            }
        }

        // GET: Workers/Create
        public ActionResult Create()
        {
            var model = new WorkersVM();
            model.Cargos = _comboHelper.GetCargosAll();
            return View(model);
        }

        // POST: Workers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,PrimerApellido,SegundoApellido,Nombres,DocumentType,Documento,FechaNacimiento,Genero,EstadoCivil,Direccion,Telefonos,FechaIngreso,TipoVinculacion,CargoID,EPS,AFP,ARL,FechaRetiro,OrganizationID,ClientID,UserID,DocumentType,Profesion,WorkArea,TipoJornada,TipoSangre,Conyuge,NumberHijos,StratumCategory,Email,TenenciaVivienda,Enfermedad,Tratamiento,SpecialRecomendations,Escolaridad")] WorkersVM model)
        {
            try
            {
                if (!ValidateModel(model))
                {
                    model.Cargos = _comboHelper.GetCargosAll();
                    return View(model);
                }
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
                model.UserID = (int)Session["userID"];
                model.FechaRetiro = Convert.ToDateTime("01/01/2090");
                if (ModelState.IsValid)
                {
                    Trabajador trabajador = await _converterHelper.ToTrabajadorAsync(model, true);
                    _empresaContext.Trabajadores.Add(trabajador);
                    await _empresaContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    model.Cargos = _comboHelper.GetCargosAll();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Trabajadores", "Index"));
            }
            //return View(model);
        }

        // GET: Workers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                Trabajador trabajador = await _empresaContext.Trabajadores.FindAsync(id);
                var model = _converterHelper.ToTrabajadorVM(trabajador);
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Trabajadores", "Delete"));
            }
        }

        // POST: Workers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,PrimerApellido,SegundoApellido,Nombres,DocumentType,Documento,FechaNacimiento,Genero,EstadoCivil,Direccion,Telefonos,FechaIngreso,TipoVinculacion,CargoID,EPS,AFP,ARL,FechaRetiro,OrganizationID,ClientID,UserID,DocumentType,Profesion,WorkArea,TipoJornada,TipoSangre,Conyuge,NumberHijos,StratumCategory,Email,TenenciaVivienda,Enfermedad,Tratamiento,SpecialRecomendations,Escolaridad")] WorkersVM model)
        {
            try
            {
                if (!ValidateModel(model))
                {
                    model.Cargos = _comboHelper.GetCargosAll();
                    return View(model);
                }

                if (ModelState.IsValid)
                {
                    Trabajador trabajador = await _converterHelper.ToTrabajadorAsync(model, false);
                    _empresaContext.Entry(trabajador).State = EntityState.Modified;
                    await _empresaContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    model.Cargos = _comboHelper.GetCargosAll();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Trabajadores", "Delete"));
            }
            //return View(model);
        }

        // GET: Workers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                Trabajador trabajador = await _empresaContext.Trabajadores.FindAsync(id);
                var model = _converterHelper.ToTrabajadorVM(trabajador);
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Trabajadores", "Delete"));
            }
        }

        // POST: Workers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Trabajador trabajador = await _empresaContext.Trabajadores.FindAsync(id);
                _empresaContext.Trabajadores.Remove(trabajador);
                await _empresaContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Trabajadores", "Delete"));
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> GenerateWorkersPdf()
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
                var item = _empresaContext.Normas.Find(organization.StandardSocioDemographic).Item;
                var fullPath = $"{_path}/2. HACER/{item}/";
                var path = Server.MapPath(fullPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Random random = new Random();
                var filename = "Perfíl Sociodemográfico" + random.Next(1, 100) + ".Pdf";
                var filePathName = path + filename;
                var list = await _empresaContext.Trabajadores
                    .Where(t => t.OrganizationID == _orgID)
                    .OrderBy(t => t.PrimerApellido)
                    .ThenBy(t => t.SegundoApellido)
                    .ThenBy(t => t.Nombres)
                    .ToListAsync();
                var modelo = _converterHelper.ToWorkersVM(list);
                var document = _empresaContext.Documents.FirstOrDefault(d => d.ID == 9);
                ViewBag.formato = document.Formato;
                ViewBag.estandar = document.Estandar;
                ViewBag.titulo = document.Titulo;
                ViewBag.version = document.Version;
                ViewBag.fecha = DateTime.Now;
                var report = new ViewAsPdf("GetAll");
                report.Model = modelo;
                report.FileName = filePathName;
                report.PageSize = Rotativa.Options.Size.A4;
                report.PageOrientation = Rotativa.Options.Orientation.Landscape;
                report.PageWidth = 399;
                report.PageHeight = 399;
                report.SaveOnServerPath = filePathName;

                //Generar archivo de movimiento
                var fullName = filename;
                var type = Path.GetExtension(filename).ToUpper();
                var descript = "Perfíl Sociodemográfico";
                var userID = (int)Session["userID"];
                Movimient movimient = new Movimient()
                {
                    ID = 0,
                    OrganizationID = _orgID,
                    NormaID = organization.StandardSocioDemographic,
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
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }
        public static bool ValidateModel(WorkersVM model)
        {
            if(model.DocumentType == 0) { return false; }
            if (model.CargoID == 0) { return false; }
            if (model.Escolaridad == 0) { return false; }
            if (model.EstadoCivil == 0) { return false; }
            if (model.Genero == 0) { return false; }
            if (model.StratumCategory == 0) { return false; }
            if (model.TenenciaVivienda == 0) { return false; }
            if (model.TipoJornada == 0) { return false; }
            if (model.TipoSangre == 0) { return false; }
            if (model.TipoVinculacion == 0) { return false; }
            if (model.WorkArea == 0) { return false; }

            return true;
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

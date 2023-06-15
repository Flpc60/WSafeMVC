using System;
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
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    public class UnsafeactsController : Controller
    {
        private int _clientID;
        private int _orgID;
        private string _year;
        private string _path;
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IGestorHelper _gestorHelper;
        private readonly IChartHelper _chartHelper;
        public UnsafeactsController
            (EmpresaContext empresaContext,
            IComboHelper comboHelper,
            IConverterHelper converterHelper,
            IGestorHelper gestorHelper, IChartHelper chartHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _gestorHelper = gestorHelper;
            _chartHelper = chartHelper;
        }

        // GET: Unsafeacts
        public async Task<ActionResult> Index()
        {
            _orgID = (int)Session["orgID"];
            var list = await _empresaContext.Unsafeacts
                    .Where(u => u.OrganizationID == _orgID)
                .OrderByDescending(u => u.FechaAntecedente)
                .ToListAsync();
            var modelo = _converterHelper.ToUnsafeactsListVM(list);
            return View(modelo);
        }

        // GET: Unsafeacts/Create
        public ActionResult Create()
        {
            var model = _converterHelper.ToUnsafeactsVMNew();
            return View(model);
        }

        // POST: Unsafeacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ZonaID,ProcesoID,ActividadID,TareaID,FechaReporte,ActCategory,Antecedentes,FechaAntecedente,CategoriaPeligroID,PeligroID,ActDescription,ProbableConsecuencia,Recomendations,WorkerID,Worker1ID,Worker2ID,MovimientID,OrganizationID,ClientID,UserID,FileName")] UnsafeactVM model, HttpPostedFileBase fileLoad)
        {
            _clientID = (int)Session["clientID"];
            _orgID = (int)Session["orgID"];
            _year = (string)Session["year"];
            _path = (string)Session["path"];
            model.ClientID = (int)Session["clientID"];
            model.OrganizationID = (int)Session["orgID"];
            model.MovimientID = 0;
            model.UserID = (int)Session["userID"];

            if (fileLoad != null)
            {
                model.FileName = $"{_path}/1. PLANEAR/2.8.1/{fileLoad.FileName}";
            }

            if (ModelState.IsValid)
            {
                model.FechaReporte = DateTime.Now;
                Unsafeact unsafeact = await _converterHelper.ToUnsafeactAsync(model, true);
                _empresaContext.Unsafeacts.Add(unsafeact);
                await _empresaContext.SaveChangesAsync();

                var item = "2.8.1";
                var normaID = 57;
                var fullPath = $"{_path}/1. PLANEAR/2.8.1/";
                var path = Server.MapPath(fullPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                fileLoad.SaveAs(path + Path.GetFileName(fileLoad.FileName));
                var fullName = fileLoad.FileName;
                var type = Path.GetExtension(fileLoad.FileName).ToUpper();

                //Generar archivo de movimiento
                var descript = "Notificación de actos y condiciones inseguras";
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
                    Ciclo = "P",
                    Type = type,
                    Path = path,
                    ClientID = _clientID
                };

                _empresaContext.Movimientos.Add(movimient);
                _empresaContext.SaveChanges();
                return RedirectToAction("Index");
            }
            model.Zonas = _comboHelper.GetComboZonas();
            model.Procesos = _comboHelper.GetComboProcesos();
            model.Actividades = _comboHelper.GetComboActividades();
            model.Tareas = _comboHelper.GetComboTareas();
            model.CategoriasPeligro = _comboHelper.GetComboCategoriaPeligros();
            model.Peligros = _comboHelper.GetComboPeligros(1);
            model.Workers = _comboHelper.GetComboTrabajadores();
            ViewBag.message = "Faltan campos por diligenciar del formulario !!";
            return View(model);
        }

        // GET: Unsafeacts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                Unsafeact unsafeact = await _empresaContext.Unsafeacts.FindAsync(id);
                var model = _converterHelper.ToUnsafeactVM(unsafeact);
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Unsafeacts", "Edit"));
            }
        }

        // POST: Unsafeacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ZonaID,ProcesoID,ActividadID,TareaID,FechaReporte,ActCategory,Antecedentes,FechaAntecedente,CategoriaPeligroID,PeligroID,ActDescription,ProbableConsecuencia,Recomendations,WorkerID,Worker1ID,Worker2ID,MovimientID,OrganizationID,ClientID,UserID")] UnsafeactVM model)
        {
            if (ModelState.IsValid)
            {
                Unsafeact unsafeact = await _converterHelper.ToUnsafeactAsync(model, false);
                _empresaContext.Entry(unsafeact).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            model.Zonas = _comboHelper.GetComboZonas();
            model.Procesos = _comboHelper.GetComboProcesos();
            model.Actividades = _comboHelper.GetComboActividades();
            model.Tareas = _comboHelper.GetComboTareas();
            model.CategoriasPeligro = _comboHelper.GetComboCategoriaPeligros();
            model.Peligros = _comboHelper.GetComboPeligros(model.CategoriaPeligroID);
            model.Workers = _comboHelper.GetComboTrabajadores();
            ViewBag.message = "Faltan campos por diligenciar del formulario !!";
            return View(model);
        }

        // GET: Unsafeacts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unsafeact unsafeact = await _empresaContext.Unsafeacts.FindAsync(id);
            if (unsafeact == null)
            {
                return HttpNotFound();
            }
            return View(unsafeact);
        }

        // POST: Unsafeacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Unsafeact unsafeact = await _empresaContext.Unsafeacts.FindAsync(id);
            _empresaContext.Unsafeacts.Remove(unsafeact);
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

        [HttpGet]
        public ActionResult GetPeligros(int id)
        {
            if (id != null)
            {
                var peligros = _comboHelper.GetComboPeligros(id);
                return Json(peligros, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
    }
}

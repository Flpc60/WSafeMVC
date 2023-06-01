using System;
using System.Data.Entity;
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
        public async Task<ActionResult> Create([Bind(Include = "ID,PrimerApellido,SegundoApellido,Nombres,DocumentType,Documento,FechaNacimiento,Genero,EstadoCivil,Direccion,Telefonos,FechaIngreso,TipoVinculacion,CargoID,EPS,AFP,ARL,FechaRetiro,OrganizationID,ClientID,UserID,DocumentType,Profesion,WorkArea,TipoJornada,TipoSangre,Conyuge,NumberHijos,StratumCategory,Email,TenenciaVivienda,Enfermedad,Tratamiento,SpecialRecomendations")] WorkersVM model)
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Trabajadores", "Index"));
            }
            return View(model);
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
        public async Task<ActionResult> Edit([Bind(Include = "ID,PrimerApellido,SegundoApellido,Nombres,DocumentType,Documento,FechaNacimiento,Genero,EstadoCivil,Direccion,Telefonos,FechaIngreso,TipoVinculacion,CargoID,EPS,AFP,ARL,FechaRetiro,OrganizationID,ClientID,UserID,DocumentType,Profesion,WorkArea,TipoJornada,TipoSangre,Conyuge,NumberHijos,StratumCategory,Email,TenenciaVivienda,Enfermedad,Tratamiento,SpecialRecomendations")] WorkersVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Trabajador trabajador = await _converterHelper.ToTrabajadorAsync(model, false);
                    _empresaContext.Entry(trabajador).State = EntityState.Modified;
                    await _empresaContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Trabajadores", "Delete"));
            }
            return View(model);
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

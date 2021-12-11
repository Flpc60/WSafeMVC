using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WSafe.Domain.Data;
using WSafe.Domain.Helpers;
using WSafe.Domain.Models;
using WSafe.Domain.Repositories.Implements;
using WSafe.Domain.Services.Implements;

namespace WSafe.Web.Controllers
{
    public class RiesgosController : Controller
    {
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        public RiesgosController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
        }

        // GET: Riesgos
        public async Task<ActionResult> Index()
        {
            var list = await _empresaContext.Riesgos.Include(z => z.Zona)
                .Include(p => p.Proceso)
                .Include(a => a.Actividad)
                .Include(t => t.Tarea)
                .Include(cp => cp.Peligro)
                .ToListAsync();

            return View(list);
        }

        // GET: Riesgos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = await _empresaContext.Riesgos.Include(z => z.Zona)
                .Include(p => p.Proceso)
                .Include(a => a.Actividad)
                .Include(t => t.Tarea)
                .Include(cp => cp.Peligro)
                .FirstOrDefaultAsync(i => i.ID == id.Value);

            if (result == null)
            {
                return HttpNotFound();
            }

            return View(result);
        }

        // GET: Riesgos/Create
        public ActionResult Create()
        {
            // TODO
            var riesgoView = new RiesgoViewModel();
            riesgoView.Procesos = _comboHelper.GetComboProcesos();
            riesgoView.Zonas = _comboHelper.GetComboZonas();
            riesgoView.Actividades = _comboHelper.GetComboActividades();
            riesgoView.Tareas = _comboHelper.GetComboTareas();
            riesgoView.CategoriasPeligros = _comboHelper.GetComboCategoriaPeligros();
            riesgoView.Peligros = _comboHelper.GetComboPeligros(1);

            return View(riesgoView);
        }

        // POST: Riesgos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RiesgoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // TODO
                model.Zonas = _comboHelper.GetComboZonas();
                model.Procesos = _comboHelper.GetComboProcesos();
                model.Actividades = _comboHelper.GetComboActividades();
                model.Tareas = _comboHelper.GetComboTareas();
                model.Peligros = _comboHelper.GetComboPeligros(1);
                model.CategoriasPeligros = _comboHelper.GetComboCategoriaPeligros();
                model.Peligros = _comboHelper.GetComboPeligros(1);

                return View(model);
            }


            if (ModelState.IsValid)
            {
                var consulta = new RiesgoService(new RiesgoRepository(_empresaContext));
                var result = await _converterHelper.ToRiesgoAsync(model, true);
                _empresaContext.Riesgos.Add(result);
                try
                {
                    await _empresaContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Riesgos", "Create"));
                }


                return RedirectToAction("Index");
            }

            return View(model);
        }


        // GET: Riesgos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = await _empresaContext.Riesgos.Include(z => z.Zona)
                .Include(p => p.Proceso)
                .Include(a => a.Actividad)
                .Include(t => t.Tarea)
                .Include(cp => cp.Peligro)
                .FirstOrDefaultAsync(i => i.ID == id.Value);

            if (result == null)
            {
                return HttpNotFound();
            }

            var consulta = new RiesgoService(new RiesgoRepository(_empresaContext));
            var riesgoViewModel = _converterHelper.ToRiesgoViewModel(result);

            if (riesgoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(riesgoViewModel);
        }

        // POST: Riesgos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RiesgoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // TODO
                model.Zonas = _comboHelper.GetComboZonas();
                model.Procesos = _comboHelper.GetComboProcesos();
                model.Actividades = _comboHelper.GetComboActividades();
                model.Tareas = _comboHelper.GetComboTareas();
                model.Peligros = _comboHelper.GetComboPeligros(1);
                model.CategoriasPeligros = _comboHelper.GetComboCategoriaPeligros();
                model.Peligros = _comboHelper.GetComboPeligros(1);

                return View(model);
            }

            if (ModelState.IsValid)
            {
                var consulta = new RiesgoService(new RiesgoRepository(_empresaContext));
                var result = await _converterHelper.ToRiesgoAsync(model, true);
                _empresaContext.Entry(result).State = EntityState.Modified;
                try
                {
                    await _empresaContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Riesgos", "Create"));
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Riesgos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var consulta = new RiesgoService(new RiesgoRepository(_empresaContext));
            var result = await consulta.GetById(id.Value);
            var riesgoViewModel = _converterHelper.ToRiesgoViewModel(result);

            if (riesgoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(riesgoViewModel);
        }


        // POST: Riesgos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var consulta = new RiesgoService(new RiesgoRepository(_empresaContext));
            var result = await consulta.GetById(id);
            try
            {
                _empresaContext.Riesgos.Remove(result);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Create"));
            }

            return RedirectToAction("Index");
        }
    }
}
﻿using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Helpers;
using WSafe.Domain.Repositories.Implements;
using WSafe.Domain.Services.Implements;
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    public class IncidentesController : Controller
    {
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IChartHelper _chartHelper;
        public IncidentesController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper, IChartHelper chartHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _chartHelper = chartHelper;
        }
        public async Task<ActionResult> Index()
        {
            var consulta = new IncidenteService(new IncidenteRepository(_empresaContext));

            return View(await consulta.GetALL());
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = await _empresaContext.Incidentes.FirstOrDefaultAsync(i => i.ID == id.Value);

            if (result == null)
            {
                return HttpNotFound();
            }

            return View(result);
        }
        public ActionResult Create()
        {
            var incidenteView = _converterHelper.ToIncidenteViewModelNew();
            return View(incidenteView);
        }

        [HttpPost]
        public async Task<ActionResult> CreateIncidente(IncidenteViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var consulta = new IncidenteService(new IncidenteRepository(_empresaContext));
                    var incidente = await _converterHelper.ToIncidenteAsync(model, true);
                    var saved = await consulta.Insert(incidente);
                    if (saved != null)
                    {
                        return Json(incidente, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { data = model, error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { data = model, error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = await _empresaContext.Incidentes
                .Include(l => l.Lesionados)
                .FirstOrDefaultAsync(i => i.ID == id.Value);

            var incidenteViewModel = _converterHelper.ToIncidenteViewModel(result);

            if (incidenteViewModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.IncidenteID = id;

            return View(incidenteViewModel);
        }

        // POST: Riesgos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(IncidenteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var consulta = new IncidenteService(new IncidenteRepository(_empresaContext));
                    var result = await _converterHelper.ToIncidenteAsync(model, false);
                    await consulta.Update(result);
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                //return View("Error", new HandleErrorInfo(ex, "Riesgos", "Create"));
                throw ex;
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

            var result = await _empresaContext.Incidentes.FirstOrDefaultAsync(i => i.ID == id.Value);

            try
            {
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Create"));
            }

            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }


        // POST: Riesgos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var consulta = new IncidenteService(new IncidenteRepository(_empresaContext));
            try
            {
                await consulta.Delete(id);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Create"));
            }

            return RedirectToAction("Index");
        }

        // GET: Riesgos/Create
        public async Task<ActionResult> CreateRiesgo(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = await _empresaContext.Riesgos.FirstOrDefaultAsync(i => i.IncidenteID == id);

            if (result != null)
            {
                return View("EditRiesgo");
            }

            var riesgoView = _converterHelper.ToRiesgoViewModelNew();
            riesgoView.IncidenteID = id;
            return View(riesgoView);
        }

        // POST: Riesgos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRiesgo(RiesgoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var consulta = new RiesgoService(new RiesgoRepository(_empresaContext));
                    var riesgo = await _converterHelper.ToRiesgoAsync(model, true);
                    var saved = await consulta.Insert(riesgo);
                    if (saved != null)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }
        public async Task<ActionResult> EditRiesgo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = await _empresaContext.Riesgos.FirstOrDefaultAsync(i => i.IncidenteID == id.Value);

            var riesgoViewModel = _converterHelper.ToRiesgoViewModel(result);

            if (riesgoViewModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.RiesgoID = id;

            return View(riesgoViewModel);
        }

        // POST: Riesgos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditRiesgo(RiesgoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var consulta = new RiesgoService(new RiesgoRepository(_empresaContext));
                    var result = await _converterHelper.ToRiesgoAsync(model, false);
                    await consulta.Update(result);
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                //return View("Error", new HandleErrorInfo(ex, "Riesgos", "Create"));
                throw ex;
            }

            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> GetAllLesionados(int idIncidente)
        {
            if (idIncidente == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lesionados = _empresaContext.Accidentados.Where(a => a.IncidenteID == idIncidente).ToList();
            var result = _converterHelper.ToListLesionadosVM(lesionados);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateLesionado(AccidentadoVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var lesionado = _empresaContext.Accidentados.FirstOrDefault(a => a.TrabajadorID == model.TrabajadorID);
                    if(lesionado == null)
                    {
                        var result = await _converterHelper.ToLesionadoAsync(model, true);
                        _empresaContext.Accidentados.Add(result);
                        var saved = await _empresaContext.SaveChangesAsync();
                        if (saved != null)
                        {
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                    }

                }
                return Json(new { data = model, error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { data = model, error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteLesionado(int id)
        {
            if (ModelState.IsValid)
            {
                Accidentado lesionado = await _empresaContext.Accidentados.FindAsync(id);
                _empresaContext.Accidentados.Remove(lesionado);
                await _empresaContext.SaveChangesAsync();
                return Json(lesionado, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index");
        }
    }
}
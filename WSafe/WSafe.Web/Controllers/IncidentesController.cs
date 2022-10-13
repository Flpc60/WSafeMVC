using Rotativa;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Data.Entities.ICAM;
using WSafe.Domain.Helpers;
using WSafe.Domain.Repositories.Implements;
using WSafe.Domain.Services.Implements;
using WSafe.Web.Filters;
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    // Controlador de incidentes / accidentes
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

        [AuthorizeUser(operation: 1, component: 3)]
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
        [AuthorizeUser(operation: 2, component: 3)]
        public ActionResult Create()
        {
            var incidenteView = _converterHelper.ToIncidenteViewModelNew();
            ViewBag.fechaReporte = DateTime.Now;
            ViewBag.fechaIncidente = incidenteView.FechaIncidente;
            return View(incidenteView);
        }

        [HttpPost]
        public async Task<ActionResult> CreateIncidente(IncidenteViewModel model)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var consulta = new IncidenteService(new IncidenteRepository(_empresaContext));
                    var incidente = await _converterHelper.ToIncidenteAsync(model, true);
                    var saved = await consulta.Insert(incidente);
                    var idIncidente = _empresaContext.Incidentes.OrderByDescending(x => x.ID).First().ID;
                    message = "El registro se ha ingresado correctamente!!";
                    return Json(new { data = idIncidente, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "El registro NO se ha ingresado correctamente!!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "El registro NO se ha ingresado correctamente!!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        [AuthorizeUser(operation: 3, component: 3)]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = await _empresaContext.Incidentes
                .Include(l => l.Lesionados)
                .FirstOrDefaultAsync(i => i.ID == id.Value);

            var model = _converterHelper.ToIncidenteViewModel(result);

            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.incidenteID = id;
            ViewBag.fechaReporte = model.FechaReporte;
            ViewBag.fechaIncidente = model.FechaIncidente;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateIncidente(IncidenteViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var consulta = new IncidenteService(new IncidenteRepository(_empresaContext));
                    var result = await _converterHelper.ToIncidenteAsync(model, false);
                    await consulta.Update(result);
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                return Json(new { data = model, error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { data = model, error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }

        [AuthorizeUser(operation: 4, component: 3)]
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
                    var lesionado = _empresaContext.Accidentados.FirstOrDefault(a => a.TrabajadorID == model.TrabajadorID && a.IncidenteID == model.IncidenteID);
                    if (lesionado == null)
                    {
                        var result = await _converterHelper.ToLesionadoAsync(model, true);
                        _empresaContext.Accidentados.Add(result);
                        var saved = await _empresaContext.SaveChangesAsync();
                        var accidenID = _empresaContext.Accidentados.OrderByDescending(x => x.ID).First().ID;
                        return Json(accidenID, JsonRequestBehavior.AllowGet);
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
            try
            {
                if (ModelState.IsValid)
                {
                    Accidentado lesionado = await _empresaContext.Accidentados.FindAsync(id);
                    _empresaContext.Accidentados.Remove(lesionado);
                    await _empresaContext.SaveChangesAsync();
                    return Json(lesionado, JsonRequestBehavior.AllowGet);
                }
                return Json(new { data = id, error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { data = id, error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [AuthorizeUser(operation: 4, component: 3)]
        public async Task<ActionResult> DeleteIncident(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var message = "";
            Incidente incidente = await _empresaContext.Incidentes.FindAsync(id);

            var model = _converterHelper.ToIncidenteViewModel(incidente);
            return Json(new { data = model, error = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteIncident(int id)
        {
            var consulta = new IncidenteService(new IncidenteRepository(_empresaContext));
            try
            {
                await consulta.Delete(id);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Acciones", "Delete"));
            }

            return Json(new { data = true, message = "El registro ha sido eliminado exitosamente" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = await _empresaContext.Incidentes.FirstOrDefaultAsync(i => i.ID == id);
            var model = _converterHelper.ToIncidentVMFull(result, 6);
            ViewBag.id = id;
            ViewBag.cantidad = model.Lesionados.Count();
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> PrintIncidentsToPdf(int id)
        {
            Random random = new Random();
            var filename = "ReporteIncidentes" + random.Next(1, 100) + ".Pdf";
            var filePathName = "~/Documents/" + filename;

            var result = await _empresaContext.Incidentes.FirstOrDefaultAsync(i => i.ID == id);
            var model = _converterHelper.ToIncidentVMFull(result, 6);
            ViewBag.id = id;
            ViewBag.cantidad = model.Lesionados.Count();
            var report = new ViewAsPdf("Details");
            report.Model = model;
            report.FileName = filePathName;
            report.PageSize = Rotativa.Options.Size.A4;
            report.Copies = 1;
            report.PageOrientation.GetValueOrDefault();

            return report;
        }

        [HttpGet]
        public ActionResult GetEvents(int id)
        {
            if (id != null)
            {
                var events = from e in _empresaContext.Events
                             where e.IncidentID == id
                             orderby e.Order
                             select new
                             {
                                 ID = e.ID,
                                 Name = e.Name.ToUpper(),
                                 Order = e.Order
                             };

                return Json(events, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCauses(int id)
        {
            if (id != null)
            {
                var result =
                    from c in _empresaContext.CausalAnalysis
                    join e in _empresaContext.Events on c.EventID equals e.ID
                    where c.IncidentID == id
                    select new
                    {
                        ID = c.ID,
                        Name = e.Name,
                        CausalFactor = c.CausalFactor,
                        PotencialFactor = c.PotencialFactor
                    };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetBarriers(int id)
        {
            if (id != null)
            {
                var result =
                    from b in _empresaContext.BarrierAnalysis
                    join e in _empresaContext.Events on b.EventID equals e.ID
                    where b.IncidentID == id
                    select new
                    {
                        ID = b.ID,
                        Name = e.Name,
                        BarrierCategory = b.BarrierCategory
                    };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetRootCausesAnalisys(int id)
        {
            if (id != null)
            {
                var result = from e in _empresaContext.RootCauses
                             where e.ID == id
                             select e;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpGet]
        public ActionResult GetAllRecomenations(int id)
        {
            if (id != null)
            {
                var result = from e in _empresaContext.Recomendations
                             where e.ID == id
                             select e;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpPost]
        public async Task<ActionResult> CreateEvent(Event model)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    message = "El evento fué ingresado exitosamente !!";
                    _empresaContext.Events.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    message = "El evento NO fué ingresado correctamente !!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                message = "El evento NO fué ingresado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateCause(CausalAnalice model)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    message = "El factor causal fué ingresado exitosamente !!";
                    _empresaContext.CausalAnalysis.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    message = "El factor causal NO fué ingresado correctamente !!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                message = "El factor causal NO fué ingresado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateBarrier(BarrierAnalice model)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    message = "La defensa fué ingresada exitosamente !!";
                    _empresaContext.BarrierAnalysis.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    message = "La defensa NO fué ingresada exitosamente !!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                message = "La defensa NO fué ingresada exitosamente !!";
                return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateRootCause(RootCause model)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    message = "La causa principal fué ingresada exitosamente !!";
                    _empresaContext.RootCauses.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    message = "La causa principal NO fué ingresada correctamente !!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                message = "La causa principal NO fué ingresada correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateRecomendation(Recomendation model)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    message = "La recomendación fué ingresada exitosamente !!";
                    _empresaContext.Recomendations.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    message = "La recomendación NO fué ingresada correctamente !!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                message = "La recomendación NO fué ingresada correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteEvent(int? id)
        {
            var message = "";
            try
            {
                var result = _empresaContext.Events.Find(id);
                return Json(new { data = result, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = "No fué posible realizar esta transacción !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteEvent(int id)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _empresaContext.Events.FindAsync(id);
                    _empresaContext.Events.Remove(result);
                    await _empresaContext.SaveChangesAsync();
                    message = "El registro fué borrado correctamente !!";
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    message = "El registro NO fué borrado correctamente !!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                message = "El registro NO fué borrado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult UpdateEvent(int ID)
        {
            var evento = _empresaContext.Events.FirstOrDefault(e => e.ID == ID);
            return Json(evento, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateEvent([Bind(Include = "ID, IncidentID, Name, Order")] Event model)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    _empresaContext.Entry(model).State = EntityState.Modified;
                    await _empresaContext.SaveChangesAsync();
                    message = "La actualización se ha realizado exitosamente !!";
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    message = "La actualización NO se ha realizado exitosamente !!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                message = "La actualización NO se ha realizado exitosamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult UpdateCause(int ID)
        {
            var cause = _empresaContext.CausalAnalysis.FirstOrDefault(c => c.ID == ID);
            return Json(cause, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCause([Bind(Include = "ID, IncidentID, EventID, CausalFactor, PotencialFactor")] CausalAnalice model)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    _empresaContext.Entry(model).State = EntityState.Modified;
                    await _empresaContext.SaveChangesAsync();
                    message = "La actualización se ha realizado exitosamente !!";
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    message = "La actualización NO se ha realizado exitosamente !!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                message = "La actualización NO se ha realizado exitosamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteCause(int? id)
        {
            var message = "";
            try
            {
                var result = _empresaContext.CausalAnalysis.Find(id);
                return Json(new { data = result, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "No fué posible realizar esta transacción !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCause(int id)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _empresaContext.CausalAnalysis.FindAsync(id);
                    _empresaContext.CausalAnalysis.Remove(result);
                    await _empresaContext.SaveChangesAsync();
                    message = "El registro fué borrado correctamente !!";
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    message = "El registro NO fué borrado correctamente !!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                message = "El registro NO fué borrado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
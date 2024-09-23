using Rotativa;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Data.Entities.ICAM;
using WSafe.Domain.Helpers;
using WSafe.Domain.Models;
using WSafe.Domain.Repositories.Implements;
using WSafe.Domain.Services.Implements;
using WSafe.Web.Filters;

namespace WSafe.Web.Controllers
{
    // Procesos de incidentes / accidentes laborales
    public class IncidentesController : Controller
    {
        private int _clientID;
        private int _orgID;
        private string _year;
        private string _path;
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
            _orgID = (int)Session["orgID"];
            var consulta = new IncidenteService(new IncidenteRepository(_empresaContext));
            var result = await consulta.GetALL();
            var model = result.Where(i => i.OrganizationID == _orgID);
            return View(model);
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
            _orgID = (int)Session["orgID"];
            var incidenteView = _converterHelper.ToIncidenteViewModelNew(_orgID);
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
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
                model.UserID = (int)Session["userID"];

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

            _orgID = (int)Session["orgID"];
            var model = _converterHelper.ToIncidenteViewModel(result, _orgID);

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
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
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
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = await _empresaContext.Riesgos.FirstOrDefaultAsync(i => i.IncidenteID == id);

            if (result != null)
            {
                return View("EditRiesgo");
            }

            _orgID = (int)Session["orgID"];
            var riesgoView = _converterHelper.ToRiesgoViewModelNew(_orgID);
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

            _orgID = (int)Session["orgID"];
            var riesgoViewModel = _converterHelper.ToRiesgoViewModel(result, _orgID);

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
        public JsonResult GetAllLesionados(int idIncidente)
        {
            if (idIncidente == 0)
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

            _orgID = (int)Session["orgID"];
            var model = _converterHelper.ToIncidenteViewModel(incidente, _orgID);
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
            if (id == 0)
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
            // Configuración nombre archivo pdf
            _clientID = (int)Session["clientID"];
            _orgID = (int)Session["orgID"];
            _year = (string)Session["year"];
            _path = (string)Session["path"];
            var organization = _empresaContext.Organizations.Find(_orgID);
            var year = _year;
            var item = _empresaContext.Normas.Find(organization.StandardIncidents).Item;
            var fullPath = $"{_path}/2. HACER/{item}/";
            var path = Server.MapPath(fullPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Random random = new Random();
            var filename = "ReporteIncidentes" + random.Next(1, 100) + ".Pdf";
            var filePathName = path + filename;
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
            report.FormsAuthenticationCookieName = FormsAuthentication.FormsCookieName;
            report.SaveOnServerPath = filePathName;

            //Generar archivo de movimiento
            var fullName = filename;
            var type = Path.GetExtension(filename).ToUpper();
            var descript = "Reporte incidentes";
            var userID = (int)Session["userID"];
            Movimient movimient = new Movimient()
            {
                ID = 0,
                OrganizationID = _orgID,
                NormaID = organization.StandardIncidents,
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

            // Generar trazabilidad 
            var model1 = _converterHelper.Traceability(organization.StandardIncidents, year, _orgID, fullName);
            if (model1 != null)
            {
                _empresaContext.SigueAnnualPlans.Add(model1);
            }

            _empresaContext.SaveChanges();
            return report;
        }

        [HttpGet]
        public ActionResult GetEvents(int id)
        {
            if (id != 0)
            {
                var events = from e in _empresaContext.Events
                             where e.IncidentID == id
                             orderby e.Order
                             select new
                             {
                                 ID = e.ID,
                                 Name = e.Name,
                                 Order = e.Order
                             };

                return Json(events, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetMainCauses(int id)
        {
            if (id != 0)
            {
                var result = _comboHelper.GetRootCauses(id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCauses(int id)
        {
            if (id != 0)
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
            if (id != 0)
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
        public ActionResult GetRootCauses(int id)
        {
            if (id != 0)
            {
                var result =
                    from r in _empresaContext.Reasons
                    join rc in _empresaContext.RootCauses on r.RootCauseID equals rc.ID
                    where rc.IncidentID == id
                    select new
                    {
                        ID = rc.ID,
                        Name = rc.Name,
                        ReasonID = r.ID,
                        Reason = r.Name
                    };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetRecomendations(int id)
        {
            if (id != 0)
            {
                var result =
                    from r in _empresaContext.Suggestions
                    join rc in _empresaContext.RootCauses on r.RootCauseID equals rc.ID
                    where r.IncidentID == id
                    select new
                    {
                        ID = rc.ID,
                        Name = rc.Name,
                        RecomendationID = r.ID,
                        Recomendation = r.Name
                    };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
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
        public async Task<ActionResult> CreateRootCause(RootCause model, string reason)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    message = "La causa principal fué ingresada exitosamente !!";
                    _empresaContext.RootCauses.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    var id = _empresaContext.RootCauses.OrderByDescending(x => x.ID).First().ID;
                    Reason result = new Reason
                    {
                        ID = 0,
                        IncidentID = model.IncidentID,
                        RootCauseID = id,
                        Name = reason
                    };
                    _empresaContext.Reasons.Add(result);
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
        public ActionResult DeleteEvent(int? id)
        {
            var message = "";
            try
            {
                var result = _empresaContext.Events.Find(id);
                return Json(new { data = result, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
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
        public JsonResult UpdateCause(int id)
        {
            var result =
                from c in _empresaContext.CausalAnalysis
                join e in _empresaContext.Events on c.EventID equals e.ID
                where c.ID == id
                select new
                {
                    ID = c.ID,
                    Event = e.ID,
                    Name = e.Name,
                    CausalFactor = c.CausalFactor,
                    PotencialFactor = c.PotencialFactor
                };

            return Json(result, JsonRequestBehavior.AllowGet);
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
        public ActionResult DeleteCause(int? id)
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

        [HttpGet]
        public JsonResult UpdateBarrier(int id)
        {
            var result =
                from b in _empresaContext.BarrierAnalysis
                join e in _empresaContext.Events on b.EventID equals e.ID
                where b.ID == id
                select new
                {
                    ID = b.ID,
                    Event = e.ID,
                    Name = e.Name,
                    BarrierCategory = b.BarrierCategory
                };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateBarrier([Bind(Include = "ID, IncidentID, EventID, BarrierCategory")] BarrierAnalice model)
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
        public ActionResult DeleteBarrier(int? id)
        {
            var message = "";
            try
            {
                var result = _empresaContext.BarrierAnalysis.Find(id);
                return Json(new { data = result, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "No fué posible realizar esta transacción !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteBarrier(int id)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _empresaContext.BarrierAnalysis.FindAsync(id);
                    _empresaContext.BarrierAnalysis.Remove(result);
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
        public JsonResult UpdateRootCause(int id)
        {
            var rootCause =
                from r in _empresaContext.Reasons
                join rc in _empresaContext.RootCauses on r.RootCauseID equals rc.ID
                where rc.ID == id
                select new
                {
                    ID = rc.ID,
                    Name = rc.Name,
                    ReasonID = r.ID,
                    Reason = r.Name
                };

            return Json(rootCause, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateRootCause([Bind(Include = "ID, IncidentID, Name")] RootCause model, int reasonID, string reason)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    _empresaContext.Entry(model).State = EntityState.Modified;
                    Reason result = _empresaContext.Reasons.Find(reasonID);
                    result.Name = reason;
                    _empresaContext.Entry(result).State = EntityState.Modified;
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
        public ActionResult DeleteRootCause(int? id)
        {
            var message = "";
            try
            {
                var result = _empresaContext.RootCauses.Find(id);
                return Json(new { data = result, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "No fué posible realizar esta transacción !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteRootCause(int id)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _empresaContext.RootCauses.FindAsync(id);
                    _empresaContext.RootCauses.Remove(result);
                    var reason = await _empresaContext.Reasons.FindAsync(id);
                    _empresaContext.Reasons.Remove(reason);
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
        public JsonResult UpdateRecomendation(int id)
        {
            var recomendation =
                from r in _empresaContext.Suggestions
                join rc in _empresaContext.RootCauses on r.RootCauseID equals rc.ID
                where r.ID == id
                select new
                {
                    ID = rc.ID,
                    Name = rc.Name,
                    RecomendationID = r.ID,
                    Recomendation = r.Name
                };

            return Json(recomendation, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateRecomendation([Bind(Include = "ID, IncidentID, RootCauseID, Name")] Recomendation model)
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
        public ActionResult DeleteRecomendation(int? id)
        {
            var message = "";
            try
            {
                var result = _empresaContext.Recomendations.Find(id);
                return Json(new { data = result, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "No fué posible realizar esta transacción !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteRecomendation(int id)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _empresaContext.Recomendations.FindAsync(id);
                    _empresaContext.Recomendations.Remove(result);
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
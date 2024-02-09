using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WSafe.Web.Models;
using WSafe.Domain.Helpers;
using WSafe.Domain.Services.Implements;
using WSafe.Domain.Repositories.Implements;
using System.IO;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Controllers
{
    public class OccupationalsController : Controller
    {
        //  Plan de trabajo anual en el SG-SST
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
                    var occupational = await _converterHelper.ToOccupationalAsync(model, true);
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

/*
        // GET: Occupationals/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalRecomendationVM medicalRecomendationVM = await db.MedicalRecomendationVMs.FindAsync(id);
            if (medicalRecomendationVM == null)
            {
                return HttpNotFound();
            }
            return View(medicalRecomendationVM);
        }

        // POST: Occupationals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ExaminationDate,Trabajador,Talla,Peso,ExaminationType,Recomendations,MedicalRecomendation")] MedicalRecomendationVM medicalRecomendationVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicalRecomendationVM).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(medicalRecomendationVM);
        }

        // GET: Occupationals/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalRecomendationVM medicalRecomendationVM = await db.MedicalRecomendationVMs.FindAsync(id);
            if (medicalRecomendationVM == null)
            {
                return HttpNotFound();
            }
            return View(medicalRecomendationVM);
        }

        // POST: Occupationals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MedicalRecomendationVM medicalRecomendationVM = await db.MedicalRecomendationVMs.FindAsync(id);
            db.MedicalRecomendationVMs.Remove(medicalRecomendationVM);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
*/
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

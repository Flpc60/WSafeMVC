using Rotativa;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Helpers;
using WSafe.Domain.Repositories.Implements;
using WSafe.Domain.Services.Implements;
using WSafe.Web.Filters;
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    public class VulnerabilitiesController : Controller
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
        private readonly IGestorHelper _gestorHelper;
        public VulnerabilitiesController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper, IChartHelper chartHelper, IGestorHelper gestorHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _chartHelper = chartHelper;
            _gestorHelper = gestorHelper;
        }

        [AuthorizeUser(operation: 1, component: 2)]
        public async Task<ActionResult> Index()
        {
            _orgID = (int)Session["orgID"];
            var repository = new VulnerabilityService(new VulnerabilityRepository(_empresaContext));
            var vulnerabilities = await repository.GetALL();
            var filtered = vulnerabilities.Where(v => v.OrganizationID == _orgID);
            var model = _converterHelper.ToListVulnerabilityVM(filtered);
            return View(model);
        }
/*
        // GET: Vulnerabilities/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VulnerabilityVM vulnerabilityVM = await db.VulnerabilityVMs.FindAsync(id);
            if (vulnerabilityVM == null)
            {
                return HttpNotFound();
            }
            return View(vulnerabilityVM);
        }

        // GET: Vulnerabilities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vulnerabilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,CategoryAmenaza,Amenaza,Type,EvaluationConcept,Item,Response,Observation")] VulnerabilityVM vulnerabilityVM)
        {
            if (ModelState.IsValid)
            {
                db.VulnerabilityVMs.Add(vulnerabilityVM);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(vulnerabilityVM);
        }

        // GET: Vulnerabilities/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VulnerabilityVM vulnerabilityVM = await db.VulnerabilityVMs.FindAsync(id);
            if (vulnerabilityVM == null)
            {
                return HttpNotFound();
            }
            return View(vulnerabilityVM);
        }

        // POST: Vulnerabilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,CategoryAmenaza,Amenaza,Type,EvaluationConcept,Item,Response,Observation")] VulnerabilityVM vulnerabilityVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vulnerabilityVM).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(vulnerabilityVM);
        }

        // GET: Vulnerabilities/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VulnerabilityVM vulnerabilityVM = await db.VulnerabilityVMs.FindAsync(id);
            if (vulnerabilityVM == null)
            {
                return HttpNotFound();
            }
            return View(vulnerabilityVM);
        }

        // POST: Vulnerabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VulnerabilityVM vulnerabilityVM = await db.VulnerabilityVMs.FindAsync(id);
            db.VulnerabilityVMs.Remove(vulnerabilityVM);
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

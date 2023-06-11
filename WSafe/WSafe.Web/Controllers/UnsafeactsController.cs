using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
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
            var modelo = _converterHelper.ToUnsafeactVM(list);
            return View(modelo);
        }

        // GET: Unsafeacts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnsafeactVM unsafeactVM = await db.UnsafeactVMs.FindAsync(id);
            if (unsafeactVM == null)
            {
                return HttpNotFound();
            }
            return View(unsafeactVM);
        }

        // GET: Unsafeacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Unsafeacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ZonaID,ProcesoID,ActividadID,TareaID,ActCategory,Antecedentes,FechaAntecednte,CategoriaPeligroID,PeligroID,ActDescription,ProbableConsecuencia,Recomendations,WorkerID,Worker1ID,Worker2ID,MovimientID,OrganizationID,ClientID")] UnsafeactVM unsafeactVM)
        {
            if (ModelState.IsValid)
            {
                db.UnsafeactVMs.Add(unsafeactVM);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(unsafeactVM);
        }

        // GET: Unsafeacts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnsafeactVM unsafeactVM = await db.UnsafeactVMs.FindAsync(id);
            if (unsafeactVM == null)
            {
                return HttpNotFound();
            }
            return View(unsafeactVM);
        }

        // POST: Unsafeacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ZonaID,ProcesoID,ActividadID,TareaID,ActCategory,Antecedentes,FechaAntecednte,CategoriaPeligroID,PeligroID,ActDescription,ProbableConsecuencia,Recomendations,WorkerID,Worker1ID,Worker2ID,MovimientID,OrganizationID,ClientID")] UnsafeactVM unsafeactVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unsafeactVM).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(unsafeactVM);
        }

        // GET: Unsafeacts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnsafeactVM unsafeactVM = await db.UnsafeactVMs.FindAsync(id);
            if (unsafeactVM == null)
            {
                return HttpNotFound();
            }
            return View(unsafeactVM);
        }

        // POST: Unsafeacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UnsafeactVM unsafeactVM = await db.UnsafeactVMs.FindAsync(id);
            db.UnsafeactVMs.Remove(unsafeactVM);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

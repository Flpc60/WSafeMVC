using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WSafe.Domain.Data;
using WSafe.Domain.Models;
using WSafe.Domain.Repositories;
using WSafe.Domain.Services;

namespace WSafe.Web.Controllers
{
    public class RiesgosController : Controller
    {
        private readonly EmpresaContext _empresaContext;
        private readonly IGenericRepository<RiesgoViewModel> _genericRepository;
        private readonly IGenericService<RiesgoViewModel> _genericService;

        public RiesgosController(EmpresaContext empresaContext, IGenericRepository<RiesgoViewModel> genericRepository
            , IGenericService<RiesgoViewModel> genericService)
        {
            _empresaContext = empresaContext;
            _genericRepository = genericRepository;
            _genericService = genericService;
        }

        // GET: Riesgos
        public async Task<ActionResult> Index()
        {
            return View(await _genericService.GetALL());
        }

        // GET: Riesgos/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiesgoViewModel riesgoViewModel = await _genericService.GetById(id);
            if (riesgoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(riesgoViewModel);
        }

        // GET: Riesgos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Riesgos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Rutinaria,EfectosPosibles,NivelDeficiencia,NivelExposicion,NivelProbabilidad,NivelConsecuencias,NivelRiesgo,NroExpuestos,RequisitoLegal")] RiesgoViewModel riesgoViewModel)
        {
            if (ModelState.IsValid)
            {
                await _genericService.Insert(riesgoViewModel);

                return RedirectToAction("Index");
            }

            return View(riesgoViewModel);
        }

        // GET: Riesgos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiesgoViewModel riesgoViewModel = await _genericService.GetById(id.Value);
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
        public async Task<ActionResult> Edit([Bind(Include = "ID,Rutinaria,EfectosPosibles,NivelDeficiencia,NivelExposicion,NivelProbabilidad,NivelConsecuencias,NivelRiesgo,NroExpuestos,RequisitoLegal")] RiesgoViewModel riesgoViewModel)
        {
            if (ModelState.IsValid)
            {
                await _genericService.Update(riesgoViewModel);
                return RedirectToAction("Index");
            }
            return View(riesgoViewModel);
        }

        // GET: Riesgos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiesgoViewModel riesgoViewModel = await _genericService.GetById(id.Value);

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
            RiesgoViewModel riesgoViewModel = await _genericService.GetById(id);
            await _genericService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
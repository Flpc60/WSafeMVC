using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WSafe.Domain.Data;
using WSafe.Domain.Models;
using WSafe.Domain.Repositories.Implements;
using WSafe.Domain.Services.Implements;

namespace WSafe.Web.Controllers
{
    public class RiesgosController : Controller
    {
        private EmpresaContext _empresaContext = new EmpresaContext();

        // GET: Riesgos
        public async Task<ActionResult> Index()
        {
            var consulta = new RiesgoService(new RiesgoRepository(_empresaContext));
            return View(await consulta.GetALL());
        }
        
        // GET: Riesgos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var consulta = new RiesgoService(new RiesgoRepository(_empresaContext));
            RiesgoViewModel riesgoViewModel = await consulta.GetById(id.Value);
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
                var consulta = new RiesgoService(new RiesgoRepository(_empresaContext));
                await consulta.Insert(riesgoViewModel);

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
            var consulta = new RiesgoService(new RiesgoRepository(_empresaContext));
            RiesgoViewModel riesgoViewModel = await consulta.GetById(id.Value);
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
                var consulta = new RiesgoService(new RiesgoRepository(_empresaContext));
                await consulta.Update(riesgoViewModel);
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
            var consulta = new RiesgoService(new RiesgoRepository(_empresaContext));
            RiesgoViewModel riesgoViewModel = await consulta.GetById(id.Value);

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
            RiesgoViewModel riesgoViewModel = await consulta.Delete(id);
            return RedirectToAction("Index");
        }
    }

}
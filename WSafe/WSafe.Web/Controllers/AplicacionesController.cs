using System.Data.Entity;
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
    public class AplicacionesController : Controller
    {
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        public AplicacionesController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
        }

        // GET: Aplicaciones
        public async Task<ActionResult> Index(int id)
        {
            var consulta = new AplicationService(new AplicationRepository(_empresaContext));
            return View(await consulta.GetALL());
        }

        // GET: Aplicaciones/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aplicacion aplicacion = await _empresaContext.Aplicaciones.FindAsync(id);
            if (aplicacion == null)
            {
                return HttpNotFound();
            }
            return View(aplicacion);
        }

        // GET: Aplicaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Aplicaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Aplicacion aplicacion)
        {
            if (ModelState.IsValid)
            {
                _empresaContext.Aplicaciones.Add(aplicacion);
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(aplicacion);
        }

        // GET: Aplicaciones/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aplicacion aplicacion = await _empresaContext.Aplicaciones.FindAsync(id);
            if (aplicacion == null)
            {
                return HttpNotFound();
            }
            return View(aplicacion);
        }

        // POST: Aplicaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Aplicacion aplicacion)
        {
            if (ModelState.IsValid)
            {
                _empresaContext.Entry(aplicacion).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(aplicacion);
        }

        // GET: Aplicaciones/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aplicacion aplicacion = await _empresaContext.Aplicaciones.FindAsync(id);
            if (aplicacion == null)
            {
                return HttpNotFound();
            }
            return View(aplicacion);
        }

        // POST: Aplicaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Aplicacion aplicacion = await _empresaContext.Aplicaciones.FindAsync(id);
            _empresaContext.Aplicaciones.Remove(aplicacion);
            await _empresaContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
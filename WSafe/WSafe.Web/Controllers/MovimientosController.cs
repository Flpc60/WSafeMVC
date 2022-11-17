using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Helpers;
using WSafe.Web.Filters;
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    public class MovimientosController : Controller
    {
        private int _operation;
        private int _roleID;
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IChartHelper _chartHelper;
        public MovimientosController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper, IChartHelper chartHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _chartHelper = chartHelper;
        }

        [AuthorizeUser(operation: 1, component: 2)]


        // GET: Movimientos
        public async Task<ActionResult> Index()
        {
            return View();
        }

        // GET: Movimientos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimient movimientVM = await _empresaContext.Movimientos.FindAsync(id);
            if (movimientVM == null)
            {
                return HttpNotFound();
            }
            return View(movimientVM);
        }

        // GET: Movimientos/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMovimient(string Descripcion, int NormaID, HttpPostedFileBase fileLoad)
        {
            var message = "";
            try
            {
                var organizatión = _empresaContext.Organizations.OrderByDescending(x => x.ID).First();
                var year = organizatión.Year.ToString();
                var normaID = NormaID.ToString();
                var descript = Descripcion.ToString();
                var norma = _empresaContext.Normas.Find(NormaID);
                var cycle = norma.Ciclo.ToString();
                var ruta = norma.Ciclo.ToString();

                switch (cycle)
                {
                    case "P":
                        ruta = "1. PLANEAR/";
                        break;

                    case "H":
                        ruta = "2. HACER/";
                        break;
                    case "V":
                        ruta = "3. VERIFICAR/";
                        break;
                    case "A":
                        ruta = "4. ACTUAR/";
                        break;
                }

                var item = norma.Item.ToString();
                var fullPath = "~/SG-SST/" + ruta + year + "/" + item + "/";
                var path = Server.MapPath(fullPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                fileLoad.SaveAs(path + Path.GetFileName(fileLoad.FileName));
                var fullName = fileLoad.FileName;

                // Crear movimiento de documentos
                Movimient model = new Movimient()
                {
                    ID = 0,
                    OrganizationID = organizatión.ID,
                    NormaID = NormaID,
                    Descripcion = descript,
                    Document = fullName,
                    Year = year,
                    Item = item,
                    Ciclo = cycle
                };

                _empresaContext.Movimientos.Add(model);
                _empresaContext.SaveChangesAsync();
                message = "El archivo ha sido creado correctamente !!";
                return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message+= ex.Message;
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> OpenFile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimient model = await _empresaContext.Movimientos.FindAsync(id);
            var message = "";
            if (model == null)
            {
                message = "El archivo NO ha sido abierto correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            var cycle = model.Ciclo.ToString();
            var ruta = model.Ciclo.ToString();

            switch (cycle)
            {
                case "P":
                    ruta = "1. PLANEAR/";
                    break;

                case "H":
                    ruta = "2. HACER/";
                    break;
                case "V":
                    ruta = "3. VERIFICAR/";
                    break;
                case "A":
                    ruta = "4. ACTUAR/";
                    break;
            }

            var year = model.Year.ToString();
            var item = model.Item.ToString();
            var fullFilePath = "~/SG-SST/" + ruta + year + "/" + item + "/" + model.Document;
            var path = Server.MapPath(fullFilePath);
            try
            {
                using (Process myProcess = new Process())
                {

                    myProcess.StartInfo.UseShellExecute = true;
                    myProcess.StartInfo.FileName = path;
                    myProcess.StartInfo.CreateNoWindow = false;
                    Process.Start(path);
                }
            }
            catch (Win32Exception w)
            {
                return Json(new { data = false, mensaj = w.Message }, JsonRequestBehavior.AllowGet);

                //Console.WriteLine(w.Message);
                //Console.WriteLine(w.ErrorCode.ToString());
                //Console.WriteLine(w.NativeErrorCode.ToString());
                //Console.WriteLine(w.StackTrace);
                //Console.WriteLine(w.Source);
                //Exception e = w.GetBaseException();
                //Console.WriteLine(e.Message);
            }
            message = "El archivo ha sido abierto correctamente !!";
            return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
        }

        // POST: Movimientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,OrganizationID,Ciclo,Item,Name,NormaID,Descripcion,Document,Year")] MovimientVM movimientVM)
        {
            if (ModelState.IsValid)
            {
                _empresaContext.Entry(movimientVM).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(movimientVM);
        }

        // GET: Movimientos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimientVM movimientVM = await _empresaContext.MovimientVMs.FindAsync(id);
            if (movimientVM == null)
            {
                return HttpNotFound();
            }
            return View(movimientVM);
        }

        // POST: Movimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MovimientVM movimientVM = await _empresaContext.MovimientVMs.FindAsync(id);
            _empresaContext.MovimientVMs.Remove(movimientVM);
            await _empresaContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult GetNormas(string ciclo)
        {
            if (ciclo != null)
            {
                var result =
                    from n in _empresaContext.Normas
                    where n.Ciclo == ciclo
                    select new
                    {
                        Value = n.ID,
                        Text = n.Item + " " + n.Name
                    };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetMovimientos(string ciclo)
        {
            if (ciclo != null)
            {
                var data = _empresaContext.Movimientos
                    .Where(m => m.Ciclo == ciclo)
                    .OrderBy(i => i.Item)
                    .ToList();
                var result = _converterHelper.ToListMovimientos(data);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}

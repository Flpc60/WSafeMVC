using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WSafe.Domain.Helpers;
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    public class IndicadoresController : Controller
    {
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IChartHelper _chartHelper;
        public IndicadoresController
            (
                EmpresaContext empresaContext,
                IComboHelper comboHelper,
                IConverterHelper converterHelper,
                IChartHelper chartHelper
            )
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _chartHelper = chartHelper;

        }

        // GET: Indicadores
        public async Task<ActionResult> Index()
        {
            var indicador = _empresaContext.Indicadores.FirstOrDefault(i => i.ID == 1);
            var result = _converterHelper.ToIndicadorViewModel(indicador);
            return View(result);
        }

        // GET: Indicadores/Create
        public ActionResult CreateNuevaConsulta()
        {
            var result = new CreateIndicatorsViewModel();
            result.Indicadores = _comboHelper.GetComboIndicadores();
            return View(result);
        }

        // POST: Indicadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNuevaConsulta(CreateIndicatorsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var indicador = _empresaContext.Indicadores.FirstOrDefault(i => i.ID == model.IndicadorID);
                var result = _converterHelper.ToIndicadorViewModelNew(indicador, model.FechaInicial, model.FechaFinal);
                return View("Index", result);
            }

            return View("Index");
        }
    }
}
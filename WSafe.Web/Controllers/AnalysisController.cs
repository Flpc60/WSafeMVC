using System.Threading.Tasks;
using System.Web.Mvc;
using WSafe.Services;

namespace WSafe.Controllers
{
    public class AnalysisController : Controller
    {
        /// <summary>
        /// Endpoints Predict, Audit, Detect. Implemetación de WSafe IA
        /// </summary>
        private readonly AnalysisApiService _apiService;

        public AnalysisController()
        {
            _apiService = new AnalysisApiService();
        }

        // GET: Analysis
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Realizar predicciones con base en inputData
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult> Predict(string inputData)
        {
            var result = await _apiService.PredictIncidentsAsync(inputData);
            ViewBag.Result = result;
            return View("Index");
        }
        /// <summary>
        /// Realizar auditorías al SG-SST
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult> Audit(string document)
        {
            var result = await _apiService.AuditDocumentsAsync(document);
            ViewBag.Result = result;
            return View("Index");
        }
        /// <summary>
        /// Detectar actos y condiciones inseguras
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult> Detect(string report)
        {
            var result = await _apiService.DetectUnsafeBehaviorsAsync(report);
            ViewBag.Result = result;
            return View("Index");
        }
    }
}

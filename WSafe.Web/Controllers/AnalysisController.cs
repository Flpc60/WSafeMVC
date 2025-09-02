using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using WSafe.Web.Services;

namespace WSafe.Web.Controllers
{
    public class AnalysisController : Controller
    {
        // POST /Analysis/Predict
        [HttpPost]
        public async Task<ActionResult> Predict(string activity, string location, int historicalWindowMonths = 12, int? topNRecommendations = null)
        {
            try
            {
                using (var api = new AnalysisApiClient())
                {
                    var payload = new
                    {
                        Activity = activity,
                        Location = location,
                        HistoricalWindowMonths = historicalWindowMonths,
                        TopNRecommendations = topNRecommendations
                    };
                    JToken result = await api.PredictAsync(payload);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST /Analysis/Audit
        [HttpPost]
        public async Task<ActionResult> Audit(string standard, string content)
        {
            try
            {
                using (var api = new AnalysisApiClient())
                {
                    var payload = new { Standard = standard, Content = content };
                    JToken result = await api.AuditAsync(payload);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST /Analysis/Detect
        [HttpPost]
        public async Task<ActionResult> Detect(string[] reports)
        {
            try
            {
                using (var api = new AnalysisApiClient())
                {
                    var payload = new { Reports = reports ?? new string[0] };
                    JToken result = await api.DetectAsync(payload);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

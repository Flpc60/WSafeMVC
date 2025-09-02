using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WSafe.Web.Services
{
    public class AnalysisApiClient : IDisposable
    {
        private readonly HttpClient _http;

        public AnalysisApiClient()
        {
            var baseUrl = Environment.GetEnvironmentVariable("AnalysisApi__BaseUrl")
                          ?? ConfigurationManager.AppSettings["AnalysisApiBaseUrl"];
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new InvalidOperationException("Configura AnalysisApiBaseUrl (env var o Web.config)");

            _http = new HttpClient { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(60) };

            // Si tu API .NET 8 requiere cabecera X-WSafe-Api-Key (propia), puedes setearla aquí:
            var apiKey = Environment.GetEnvironmentVariable("WSAFE_API_KEY")
                         ?? ConfigurationManager.AppSettings["WSafeApiKey"];
            if (!string.IsNullOrWhiteSpace(apiKey))
                _http.DefaultRequestHeaders.Add("X-WSafe-Api-Key", apiKey);
        }

        public Task<JToken> PredictAsync(object payload) => PostAsync("/api/analysis/predict", payload);
        public Task<JToken> AuditAsync(object payload) => PostAsync("/api/analysis/audit", payload);
        public Task<JToken> DetectAsync(object payload) => PostAsync("/api/analysis/detect", payload);

        private async Task<JToken> PostAsync(string path, object payload)
        {
            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await _http.PostAsync(path, content);
            var body = await resp.Content.ReadAsStringAsync();
            if (!resp.IsSuccessStatusCode)
                throw new InvalidOperationException($"API {resp.StatusCode}: {body}");
            return string.IsNullOrWhiteSpace(body) ? JValue.CreateNull() : JToken.Parse(body);
        }

        public void Dispose() => _http?.Dispose();
    }
}

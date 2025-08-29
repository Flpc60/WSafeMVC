using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WSafe.Services
{
    public class AnalysisApiService
    {
        private readonly HttpClient _httpClient;

        public AnalysisApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001/api/analysis/") // ⚡ cambia al puerto real de tu API externa
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> PredictIncidentsAsync(string inputData)
        {
            var response = await _httpClient.PostAsJsonAsync("predict-incidents", new { input = inputData });
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> AuditDocumentsAsync(string docContent)
        {
            var response = await _httpClient.PostAsJsonAsync("audit-documents", new { document = docContent });
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DetectUnsafeBehaviorsAsync(string report)
        {
            var response = await _httpClient.PostAsJsonAsync("detect-unsafe-behaviors", new { report });
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}

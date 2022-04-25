using RequestHandler.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.ModelHandlers
{
    public class PdfReportHandler : IPdfReportHandler
    {
        private readonly string controllerUrl = "/api/PdfReports";

        private readonly IHttpClientFactoryHandler _httpClientHandler;
        public PdfReportHandler(IHttpClientFactoryHandler httpClientHandler)
        {
            _httpClientHandler = httpClientHandler;
        }

        public async Task<HttpResponseMessage> GetAllPdLogs()
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.GetAsync(controllerUrl);
        }

        public async Task<HttpResponseMessage> GetPdfDocumentById(string documentId)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.GetAsync(controllerUrl + $"/{documentId}");
        }
    }
}

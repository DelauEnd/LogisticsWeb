using Logistics.Models.PdfModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RequestHandler.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CargoTransportation.Controllers
{
    [Authorize]
    [Route("PdfReports")]
    public class PdfReportsController : Controller
    {
        private readonly IPdfReportHandler _pdfReportHandler;

        public PdfReportsController(IPdfReportHandler pdfReportHandler)
        {
            _pdfReportHandler = pdfReportHandler;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var response = await _pdfReportHandler.GetAllPdLogs();

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            var pdfLogs = JsonConvert.DeserializeObject<IEnumerable<OrderPdfLog>>(await response.Content.ReadAsStringAsync());


            return View(pdfLogs);
        }

        [HttpGet("{documentId}")]
        public async Task<ActionResult> GetDocumentById(string documentId)
        {
            var response = await _pdfReportHandler.GetPdfDocumentById(documentId);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            var pdfBytes = JsonConvert.DeserializeObject<byte[]>(await response.Content.ReadAsStringAsync());

            var stream = new MemoryStream();
            await stream.WriteAsync(pdfBytes, 0, pdfBytes.Length);
            stream.Position = 0;

            return new FileStreamResult(stream, "application/pdf");
        }
    }
}
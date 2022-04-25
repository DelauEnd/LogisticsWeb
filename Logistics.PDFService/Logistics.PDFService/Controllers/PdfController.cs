using Logistics.PdfService.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Logistics.PdfService.Controllers
{
    [Route("api/PdfReports"), Authorize]
    [ApiController]
    public class PdfController : Controller
    {
        private readonly IOrderPdfLogRepository _logRepository;
        private readonly IOrderPdfRepository _orderPdfRepository;

        public PdfController(IOrderPdfLogRepository logRepository, IOrderPdfRepository orderPdfRepository)
        {
            _logRepository = logRepository;
            _orderPdfRepository = orderPdfRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPdfLogs()
        {
            return Ok(await _logRepository.GetAllPdfLogs());
        }

        [HttpGet("{documentId}")]
        public async Task<IActionResult> GetPdfByDocumentId(string documentId)
        {
            var pdf = await _orderPdfRepository.GetOrderPdfById(documentId);
            return Ok(pdf.PdfFile);
        }
    }
}

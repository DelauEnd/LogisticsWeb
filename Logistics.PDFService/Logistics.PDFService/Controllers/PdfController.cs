using Logistics.PdfService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Logistics.PdfService.Controllers
{
    [Route("api/PdfReports")]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPdfByDocumentId(string id)
        {
            var pdf = await _orderPdfRepository.GetOrderPdfById(id);
            return Ok(pdf.PdfFile);
        }
    }
}

using iText.IO.Source;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Logistics.Models.ResponseDTO;
using Logistics.PdfService.Services.Interfaces;
using Logistics.PDFService.Models;
using Logistics.PDFService.Services.Interfaces;
using System.Threading.Tasks;

namespace Logistics.PDFService.Services
{
    public class OrderPdfGen : IOrderPdfGen
    {
        private readonly IOrderPdfRepository _orderPdfService;
        public OrderPdfGen(IOrderPdfRepository orderPdfService)
        {
            _orderPdfService = orderPdfService;
        }

        public async Task GenOrderPdf(OrderDto order)
        {
            ByteArrayOutputStream baos = new ByteArrayOutputStream();
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(baos));

            Document document = new Document(pdfDoc);

            Paragraph header = new Paragraph("HEADER")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);

            document.Add(header);
            document.Close();

            var bytes = baos.ToArray();

            var orderPdf = new OrderPdf
            {
                CreationDate = System.DateTime.Now,
                PdfFile = bytes
            };

            await _orderPdfService.AddOrderPdf(orderPdf);
        }
    }
}

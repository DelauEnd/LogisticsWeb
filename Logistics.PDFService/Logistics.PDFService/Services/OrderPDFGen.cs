using iText.IO.Source;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Logistics.Models.ResponseDTO;
using Logistics.PDFService.Interfaces;
using System.Threading.Tasks;

namespace Logistics.PDFService.Services
{
    public class OrderPDFGen : IOrderPDFGen
    {
        public string GenOrderPDF(OrderDto order)
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
            var res = "";

            foreach (var num in bytes)
                res += num + ", ";

            return res;
        }
    }
}

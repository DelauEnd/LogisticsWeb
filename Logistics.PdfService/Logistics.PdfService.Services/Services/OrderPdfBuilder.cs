using iText.IO.Source;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Logistics.Models.ResponseDTO;
using Logistics.PdfService.Services.Interfaces;
using Logistics.PdfService.Models;
using System;
using System.Threading.Tasks;

namespace Logistics.PdfService.Services
{
    public class OrderPdfBuilder : IOrderPdfBuilder
    {
        public async Task<OrderPdf> BuildOrderPdf(OrderDto order)
        {
            ByteArrayOutputStream baos = await Task.Run(() => CreateDocument(order));      
            var bytes = baos.ToArray();

            var orderPdf = new OrderPdf
            {
                PdfFile = bytes
            };
            return orderPdf;
        }

        private ByteArrayOutputStream CreateDocument(OrderDto order)
        {
            ByteArrayOutputStream baos = new ByteArrayOutputStream();
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(baos));

            Document document = new Document(pdfDoc);

            Paragraph header = new Paragraph("ORDER REPORT")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20)
                .SetMarginBottom(10);

            LineSeparator separatorLine = new LineSeparator(new SolidLine());          

            Paragraph senderText = new Paragraph($"Sender address: {order.Sender}")
               .SetTextAlignment(TextAlignment.LEFT)
               .SetFontSize(16)
               .SetMarginBottom(5);

            Paragraph destinationText = new Paragraph($"Destination address: {order.Destination}")
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(16)
                .SetMarginBottom(10);

            Paragraph orderStatusText = new Paragraph($"Order status: {order.Status}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(16)
                .SetMarginBottom(10);

            document.Add(header);
            document.Add(separatorLine);
            document.Add(senderText);
            document.Add(destinationText);
            document.Add(orderStatusText);
            document.Close();

            return baos;
        }
    }
}

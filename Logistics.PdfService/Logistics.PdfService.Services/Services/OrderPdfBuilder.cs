using iText.IO.Image;
using iText.IO.Source;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Logistics.Models.BrokerModels;
using Logistics.PdfService.Models.Models;
using Logistics.PdfService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.PdfService.Services.Services
{
    public class OrderPdfBuilder : IOrderPdfBuilder
    {
        public async Task<OrderPdf> BuildOrderPdf(CreatedOrderMessage order)
        {
            var documentBytes = await Task.Run(() => CreateDocument(order));

            var orderPdf = new OrderPdf
            {
                PdfFile = documentBytes
            };

            return orderPdf;
        }

        private byte[] CreateDocument(CreatedOrderMessage order)
        {
            ByteArrayOutputStream baos = new ByteArrayOutputStream();
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(baos));

            Document document = new Document(pdfDoc);

            string logoImageFile = $"{Directory.GetCurrentDirectory()}\\Resourcess\\Logo.png";
            ImageData logoImgData = ImageDataFactory.Create(logoImageFile);
            Image logoImg = new Image(logoImgData).ScaleToFit(100, 100)
                .SetFixedPosition(36, 758);

            Paragraph headerDiv = new Paragraph("ORDER REPORT")
                .SetHeight(40)
                .SetFontSize(20)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetTextAlignment(TextAlignment.CENTER);

            LineSeparator separatorLine = new LineSeparator(new SolidLine());

            Paragraph orderRecieverText = new Paragraph(@$"Order customer: {order.Destination.Surname} {order.Destination.Name} {order.Destination.Patronymic}")
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(14);

            Paragraph orderRecieverPhoneNumberText = new Paragraph(@$"Phone number: {order.Destination.PhoneNumber}")
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(14)
                .SetMarginBottom(10);

            Paragraph senderText = new Paragraph($"From address: {order.SenderAddress}")
               .SetTextAlignment(TextAlignment.LEFT)
               .SetFontSize(14);

            Paragraph destinationText = new Paragraph($"To address: {order.DestinationAddress}")
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(14)
                .SetMarginBottom(10);

            Paragraph cargoesHeader = new Paragraph("CARGOES TO HANDLE")
                .SetFontSize(20)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetTextAlignment(TextAlignment.CENTER);

            Paragraph orderStatusText = new Paragraph($"Order status: {order.Status}")
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(16)
                .SetMarginBottom(10);

            Div orderInfoDiv = new Div()
                .Add(separatorLine)
                .Add(orderRecieverText)
                .Add(orderRecieverPhoneNumberText)
                .Add(senderText)
                .Add(destinationText)
                .Add(separatorLine);

            Div cargoesInfoDiv = new Div();

            var cargoColumns = new Paragraph($@"Number | Title")
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetFontSize(14);

            cargoesInfoDiv
                .Add(separatorLine)
                .Add(cargoColumns);

            if(order.Cargoes.Count() > 0)
                foreach (var cargo in order.Cargoes)
                {
                    var cargoInfo = new Paragraph($@"{cargo.Id} | {cargo.Title}")
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFontSize(14);

                    cargoesInfoDiv.Add(cargoInfo);
                }
            else
            {
                var cargoInfo = new Paragraph($@"No cargoes registered yet")
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFontSize(14);

                cargoesInfoDiv.Add(cargoInfo);
            }

            cargoesInfoDiv
                .Add(separatorLine);

            document.Add(logoImg)
                .Add(headerDiv)
                .Add(orderInfoDiv)
                .Add(cargoesHeader)
                .Add(cargoesInfoDiv)
                .Add(orderStatusText)
                .Close();

            return baos.ToArray();
        }
    }
}

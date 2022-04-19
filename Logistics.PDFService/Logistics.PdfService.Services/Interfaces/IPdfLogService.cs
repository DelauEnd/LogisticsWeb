﻿using Logistics.Models.BrokerModels;
using Logistics.PdfService.Models.Models;

namespace Logistics.PdfService.Services.Interfaces
{
    public interface IPdfLogService
    {
        public OrderPdfLog CreatePdfLog(CreatedOrderMessage createdOrderMessage, OrderPdf orderPdf);
    }
}

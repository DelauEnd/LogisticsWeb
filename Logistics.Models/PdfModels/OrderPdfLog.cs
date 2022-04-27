using Logistics.Models.Enums;
using System;

namespace Logistics.Models.PdfModels
{
    public class OrderPdfLog
    {
        public int LogId { get; set; }
        public DateTime LogDate { get; set; }
        public int OrderId { get; set; }
        public string DocumentId { get; set; }
        public string OrderSenderSurname { get; set; }
        public string OrderSenderAddress { get; set; }
        public string OrderRecieverSurname { get; set; }
        public string OrderRecieverAddress { get; set; }
        public OperationType OperationType { get; set; }
    }
}

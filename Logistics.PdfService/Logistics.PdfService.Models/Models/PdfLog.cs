using Logistics.PdfService.Models.Enum;
using System;

namespace Logistics.PdfService.Models
{
    public class PdfLog
    {
        public int LogId { get; set; }
        public DateTime LogDate { get; set; }
        public string DocumentId { get; set; }
        public OperationType OperationType { get; set; }
    }
}

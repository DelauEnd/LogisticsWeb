using System;

namespace Logistics.PdfService.Entities.Models
{
    public class PdfLog
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string DocumentId { get; set; }
    }
}

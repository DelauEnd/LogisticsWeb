using Logistics.PdfService.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.PdfService.Entities
{
    public class LogPdfDbContext : DbContext
    {
        public DbSet<PdfLog> PdfLogs { get; set; }

        public LogPdfDbContext(DbContextOptions options)
            : base(options) { }
    }
}

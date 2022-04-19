using Logistics.Entities.Configuration;
using Logistics.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Entities
{
    public class LogisticsDbContext : DbContext
    {
        public DbSet<Route> Routes { get; set; }
        public DbSet<Transport> Transports { get; set; }

        public LogisticsDbContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplyConfigurations(modelBuilder);
        }

        private void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new TransportConfiguration());
            modelBuilder.ApplyConfiguration(new CargoCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new CargoConfiguration());
            modelBuilder.ApplyConfiguration(new RouteConfiguration());
        }
    }
}

using Logistics.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Entities.Configuration
{
    public class RouteConfiguration : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            AddInitialData(builder);
        }

        private void AddInitialData(EntityTypeBuilder<Route> builder)
        {
            builder.HasData
            (
                new Route
                {
                    Id = 1,
                    TransportId = 1
                }
            );
        }
    }
}

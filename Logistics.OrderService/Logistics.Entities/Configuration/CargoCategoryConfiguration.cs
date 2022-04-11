using Logistics.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Entities.Configuration
{
    public class CargoCategoryConfiguration : IEntityTypeConfiguration<CargoCategory>
    {
        public void Configure(EntityTypeBuilder<CargoCategory> builder)
        {
            AddInitialData(builder);
        }

        private void AddInitialData(EntityTypeBuilder<CargoCategory> builder)
        {
            builder.HasData
            (
                new CargoCategory
                {
                    Id = 1,
                    Title = "Initial Category"
                }
            );
        }
    }
}

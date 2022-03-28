using Logistics.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.IdentityServer.Entities.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            AddInitialData(builder);
        }

        private void AddInitialData(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData
            (
                new IdentityRole
                {
                    Name = nameof(UserRole.Manager),
                    NormalizedName = nameof(UserRole.Manager).ToUpper()
                },
                new IdentityRole
                {
                    Name = nameof(UserRole.Administrator),
                    NormalizedName = nameof(UserRole.Administrator).ToUpper()
                }
            );
        }
    }
}

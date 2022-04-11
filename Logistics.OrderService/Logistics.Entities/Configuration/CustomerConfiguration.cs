using Logistics.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Entities.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            AddInitialData(builder);
        }

        private void AddInitialData(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData
            (
                new Customer
                {
                    Id = 1,
                    Address = "14681 Longview Dr, Loxley, AL, 36551 ",
                }
            );

            builder.OwnsOne(customer => customer.ContactPerson).HasData
            (
                new
                {
                    CustomerId = 1,
                    Name = "Pasha",
                    Surname = "Trikorochki",
                    Patronymic = "Olegovich",
                    PhoneNumber = "86(4235)888-11-34"
                }
            );
        }
    }
}

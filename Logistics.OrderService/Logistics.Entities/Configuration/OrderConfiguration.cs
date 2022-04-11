using Logistics.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Entities.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            ConfigureModel(builder);
            AddInitialData(builder);
        }

        private void ConfigureModel(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(order => order.Destination)
                .WithMany(customer => customer.OrderDestination)
                .HasForeignKey(order => order.DestinationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(order => order.Sender)
                 .WithMany(customer => customer.OrderSender)
                 .HasForeignKey(order => order.SenderId)
                 .OnDelete(DeleteBehavior.NoAction);
        }

        private void AddInitialData(EntityTypeBuilder<Order> builder)
        {
            builder.HasData
             (
                new Order
                {
                    Id = 1,
                    DestinationId = 1,
                    SenderId = 1,
                    Status = Enums.Status.Processing,
                }
            );
        }

    }
}

using Logistics.Entities.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistics.Entities.Models
{
    public class Order : IEntity
    {
        [Key]
        [Column("OrderId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Status - required field")]
        public Status Status { get; set; }

        [Required(ErrorMessage = "SenderId - required field")]
        public int SenderId { get; set; }

        public Customer Sender { get; set; }

        [Required(ErrorMessage = "DestinationId - required field")]
        public int DestinationId { get; set; }

        public Customer Destination { get; set; }

        public List<Cargo> Cargoes { get; set; }
    }
}

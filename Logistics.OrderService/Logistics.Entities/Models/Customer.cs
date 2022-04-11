using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistics.Entities.Models
{
    public class Customer : IEntity
    {
        [Key]
        [Column("CustomerId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Address - required field")]
        public string Address { get; set; }

        [Required(ErrorMessage = "ContactPerson info - required fields")]
        public Person ContactPerson { get; set; }

        public List<Order> OrderSender { get; set; }
        public List<Order> OrderDestination { get; set; }
    }
}

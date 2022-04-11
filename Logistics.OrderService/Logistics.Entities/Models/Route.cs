using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistics.Entities.Models
{
    public class Route : IEntity
    {
        [Key]
        [Column("RouteId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "TransportId - required field")]
        [ForeignKey(nameof(Transport))]
        public int TransportId { get; set; }

        public Transport Transport { get; set; }

        public List<Cargo> Cargoes { get; set; }
    }
}

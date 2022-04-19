using Logistics.Entities.Models.OwnedModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistics.Entities.Models
{
    public class Transport : IEntity
    {
        [Key]
        [Column("TransportId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "RegistrationNumber - required field")]
        [MaxLength(30, ErrorMessage = "RegistrationNumber max length - 30 simbols.")]
        public string RegistrationNumber { get; set; }

        [Required(ErrorMessage = "LoadCapacity - required field")]
        public double LoadCapacity { get; set; }

        [Required(ErrorMessage = "Driver info - required fields")]
        public Person Driver { get; set; }

        public List<Route> Route { get; set; }
    }
}

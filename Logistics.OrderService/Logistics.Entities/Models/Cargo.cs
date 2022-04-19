using Logistics.Entities.Models.OwnedModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistics.Entities.Models
{
    public class Cargo : IEntity
    {
        [Key]
        [Column("CargoId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title - required field")]
        [MaxLength(30, ErrorMessage = "Title max length - 30 simbols.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "DepartureDate - required field")]
        public DateTime DepartureDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "ArrivalDate - required field")]
        public DateTime ArrivalDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Weight - required field")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Dimensions - required fields")]
        public Dimensions Dimensions { get; set; }

        [Required(ErrorMessage = "Image - required fields")]
        public byte[] Image { get; set; }

        [Required(ErrorMessage = "CategoryId - required field")]
        [ForeignKey(nameof(CargoCategory))]
        public int CategoryId { get; set; }
        public CargoCategory Category { get; set; }

        [Required(ErrorMessage = "OrderId - required field")]
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey(nameof(Route))]
        public int? RouteId { get; set; }
        public Route Route { get; set; }
    }
}

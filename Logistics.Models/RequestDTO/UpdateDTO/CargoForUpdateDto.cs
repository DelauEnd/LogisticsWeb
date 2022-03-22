using Logistics.Models.OwnedModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace Logistics.Models.RequestDTO.UpdateDTO
{
    public class CargoForUpdateDto
    {
        [MaxLength(30, ErrorMessage = "Title max length - 30 simbols.")]
        public string Title { get; set; }

        public int CategoryId { get; set; }

        public int? RouteId { get; set; }

        public DateTime DepartureDate { get; set; } = DateTime.Now;

        public DateTime ArrivalDate { get; set; } = DateTime.Now;

        public byte[] Image { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Weight - required field and can not be less then 0")]
        public double Weight { get; set; }

        public Dimensions Dimensions { get; set; }
    }
}

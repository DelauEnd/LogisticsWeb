using Logistics.Models.OwnedModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace Logistics.Models.RequestDTO.CreateDTO
{
    public class CargoForCreationDto
    {
        [Required(ErrorMessage = "Title - required field")]
        [MaxLength(30, ErrorMessage = "Title max length - 30 simbols.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "CategoryId - required field")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "DepartureDate - required field")]
        public DateTime DepartureDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "ArrivalDate - required field")]
        public DateTime ArrivalDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Weight - required field")]
        [Range(0, double.MaxValue, ErrorMessage = "Weight - required field and can not be less then 0")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Dimensions - required fields")]
        public Dimensions Dimensions { get; set; }

        [Required(ErrorMessage = "Image - required fields")]
        public byte[] Image { get; set; }
    }
}

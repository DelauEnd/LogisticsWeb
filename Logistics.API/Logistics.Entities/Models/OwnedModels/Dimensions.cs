using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Logistics.Entities.Models.OwnedModels
{
    [Owned]
    public class Dimensions
    {
        [Range(0, double.MaxValue, ErrorMessage = "Length - required field and can not be less then 0")]
        public double Length { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Height - required field and can not be less then 0")]
        public double Height { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Width - required field and can not be less then 0")]
        public double Width { get; set; }
    }
}

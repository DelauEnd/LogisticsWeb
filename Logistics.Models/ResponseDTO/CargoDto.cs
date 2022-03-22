using Logistics.Models.OwnedModels;
using System;

namespace Logistics.Models.ResponseDTO
{
    public class CargoDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public double Weight { get; set; }

        public Dimensions Dimensions { get; set; }

        public byte[] Image { get; set; }
    }
}

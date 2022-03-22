using Logistics.Models.OwnedModels;

namespace Logistics.Models.ResponseDTO
{
    public class TransportDto
    {
        public int Id { get; set; }

        public string RegistrationNumber { get; set; }

        public double LoadCapacity { get; set; }

        public Person Driver { get; set; }
    }
}

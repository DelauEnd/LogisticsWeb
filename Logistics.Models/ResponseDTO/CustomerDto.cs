using Logistics.Models.OwnedModels;

namespace Logistics.Models.ResponseDTO
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public Person ContactPerson { get; set; }
    }
}

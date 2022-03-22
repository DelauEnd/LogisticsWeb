using Logistics.Models.OwnedModels;
using System.ComponentModel.DataAnnotations;

namespace Logistics.Models.RequestDTO.UpdateDTO
{
    public class CustomerForUpdateDto
    {
        [MaxLength(30, ErrorMessage = "Address max length - 30 simbols.")]
        public string Address { get; set; }
        public Person ContactPerson { get; set; }
    }
}

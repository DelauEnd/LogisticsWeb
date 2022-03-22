using System.ComponentModel.DataAnnotations;

namespace Logistics.Models.RequestDTO.CreateDTO
{
    public class RouteForCreationDto
    {
        [Required(ErrorMessage = "TransportId - required field")]
        public string TransportRegistrationNumber { get; set; }
    }
}

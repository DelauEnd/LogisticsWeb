using Logistics.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Logistics.Models.RequestDTO.UpdateDTO
{
    public class OrderForUpdateDto
    {
        public int SenderId { get; set; }

        public int DestinationId { get; set; }

        [EnumDataType(typeof(Status))]
        public string Status { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Logistics.Models.RequestDTO.UpdateDTO
{
    public class CargoCategoryForUpdateDto
    {
        [MaxLength(30, ErrorMessage = "Title max length - 30 simbols.")]
        public string Title { get; set; }
    }
}

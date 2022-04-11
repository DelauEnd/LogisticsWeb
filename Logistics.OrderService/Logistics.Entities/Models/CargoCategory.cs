using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistics.Entities.Models
{
    public class CargoCategory : IEntity
    {
        [Key]
        [Column("CategoryId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title - required field")]
        [MaxLength(30, ErrorMessage = "Title max length - 30 simbols.")]
        public string Title { get; set; }

        public List<Cargo> Cargoes { get; set; }
    }
}

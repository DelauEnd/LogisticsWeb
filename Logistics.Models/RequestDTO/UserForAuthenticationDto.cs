using System.ComponentModel.DataAnnotations;

namespace Logistics.Models.RequestDTO
{
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "UserName - required field")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password - required field")]
        public string Password { get; set; }

    }
}

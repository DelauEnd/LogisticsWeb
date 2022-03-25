using System.Collections.Generic;

namespace Logistics.Models.ResponseDTO
{
    public class AuthenticatedUserInfo
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}

using System.Collections.Generic;

namespace Logistics.Models.ResponseDTO
{
    public class AuthenticatedUserInfo
    {
        public string AuthToken { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}

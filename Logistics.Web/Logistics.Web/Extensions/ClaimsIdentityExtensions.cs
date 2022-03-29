using System;
using System.Linq;
using System.Security.Claims;

namespace Logistics.Web.Extensions
{
    public static class ClaimsIdentityExtensions
    {
        public static bool UserInRole(this ClaimsIdentity claims, string requiredRole)
        {
            var roles = claims.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);

            return roles.Contains(requiredRole);
        }
    }
}

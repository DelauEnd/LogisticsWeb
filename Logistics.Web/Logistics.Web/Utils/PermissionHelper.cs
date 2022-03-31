using Logistics.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;


public class PermissionsHelper
{
    private readonly IEnumerable<Claim> _claims;
    private readonly IEnumerable<string> _roles;
    public PermissionsHelper(IHttpContextAccessor httpContextAccessor)
    {
        _claims = httpContextAccessor.HttpContext.User.Claims;
        _roles = _claims.Where(i => i.Type == ClaimTypes.Role).Select(x => x.Value);
    }

    public bool IsAdmin()
    {
        return _roles.Contains(nameof(UserRole.Administrator));
    }

    public bool IsManager()
    {
        return _roles.Contains(nameof(UserRole.Manager));
    }
}

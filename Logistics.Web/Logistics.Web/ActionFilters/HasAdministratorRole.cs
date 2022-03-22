using RequestHandler;

namespace CargoTransportation.ActionFilters
{
    public class HasAdministratorRole : RoleAttributeBase
    {
        protected override string RequiredRole { get; set; }

        public HasAdministratorRole(IRequestManager request) : base(request)
        {
            RequiredRole = "Administrator";
        }
    }
}

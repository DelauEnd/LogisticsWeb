using RequestHandler;

namespace CargoTransportation.ActionFilters
{
    public class HasManagerRole : RoleAttributeBase
    {
        protected override string RequiredRole { get; set; }

        public HasManagerRole(IRequestManager request) : base(request)
        {
            RequiredRole = "Manager";
        }
    }
}

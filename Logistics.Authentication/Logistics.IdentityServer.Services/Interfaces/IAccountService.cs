using Logistics.Models.RequestDTO;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.ResponseDTO;
using System.Threading.Tasks;

namespace Logistics.IdentityServer.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<AuthenticatedUserInfo> AuthenticateUser(UserForAuthenticationDto user);
        public Task CreateUser(UserForCreationDto userForCreation);
        public Task AddRoleToUser(string login, string role);
    }
}

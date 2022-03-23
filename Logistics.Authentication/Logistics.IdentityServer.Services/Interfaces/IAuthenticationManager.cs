using Logistics.IdentityServer.Entities.Models;
using Logistics.Models.ResponseDTO;
using System.Threading.Tasks;

namespace Logistics.IdentityServer.Services.Interfaces
{
    public interface IAuthenticationManager
    {
        Task<User> ReturnUserIfValid(UserForAuthenticationDto userForAuthentication);
        Task<string> CreateToken(User user);
    }
}

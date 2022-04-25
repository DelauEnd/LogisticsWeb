using Logistics.Models.IdentityModels;
using Logistics.Models.RequestDTO;
using System.Threading.Tasks;

namespace Logistics.IdentityServer.Services.Interfaces
{
    public interface IAuthenticationManager
    {
        Task<User> ReturnUserIfValid(UserForAuthenticationDto userForAuthentication);
        Task<(string accessToken, string refreshToken)> GetTokens(UserForAuthenticationDto user);
    }
}

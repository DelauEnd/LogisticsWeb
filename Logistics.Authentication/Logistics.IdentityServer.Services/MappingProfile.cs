using AutoMapper;
using Logistics.IdentityServer.Entities.Models;
using Logistics.Models.RequestDTO.CreateDTO;

namespace Logistics.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateUserMaps();
        }
     
        private void CreateUserMaps()
        {
            CreateMap<UserForCreationDto, User>();
        }
    }
}

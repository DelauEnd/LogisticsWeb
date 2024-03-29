﻿using AutoMapper;
using Logistics.Models.IdentityModels;
using Logistics.Models.RequestDTO.CreateDTO;

namespace Logistics.IdentityServer.Services
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

﻿using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.ResponseDTO;
using System.Threading.Tasks;

namespace Logistics.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<AuthenticatedUserInfo> AuthenticateUser(UserForAuthenticationDto user);
        public Task CreateUser(UserForCreationDto userForCreation);
        public Task AddRoleToUser(string login, string role);
    }
}

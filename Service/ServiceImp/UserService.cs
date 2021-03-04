using Core.IRepositories;
using Core.MessageResponse;
using Core.Models;
using Core.Models.User;
using Infrustructures.Repositories;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Service.ServiceImp
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService()
        {
            _repository = new UserRepository();
        }

        public async Task<ServiceResponse<int>> CreateUser(CreateUserRequest request)
        {
            var result = await _repository.CreateUser(request);
            return new ServiceResponse<int>(result, string.Empty, "success", false);
        }

        public async Task<ServiceResponse<UserDetail>> GetUser(UserRequest request)
        {
            var result = await _repository.GetUser(request);
            return new ServiceResponse<UserDetail>(result, string.Empty, "success", false);
        }

        public async Task<ServiceResponse<UserDto>> GetUserInfo(UserInformationRequest request)
        {
            var result = await _repository.GetUserInfo(request);
            return new ServiceResponse<UserDto>(result, string.Empty, "success", false);
        }
    }
}
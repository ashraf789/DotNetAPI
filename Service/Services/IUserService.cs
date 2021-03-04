using Core.MessageResponse;
using Core.Models;
using Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Service.Services
{
    public interface IUserService : IBaseService
    {
        Task<ServiceResponse<int>> CreateUser(CreateUserRequest request);
        Task<ServiceResponse<UserDetail>> GetUser(UserRequest request);
        Task<ServiceResponse<UserDto>> GetUserInfo(UserInformationRequest request);
    }
}
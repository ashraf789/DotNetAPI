using Core.Models;
using Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Core.IRepositories
{
    public interface IUserRepository
    {
        #region authentication
        Task<int> CreateUser(CreateUserRequest request);
        Task<UserDetail> GetUser(UserRequest request);
        #endregion
    }
}
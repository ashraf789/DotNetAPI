using Common;
using Core.IRepositories;
using Core.Models;
using Core.Models.User;
using Dapper;
using Infrustructures.MySqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Infrustructures.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<int> CreateUser(CreateUserRequest request)
        {
            using (var dbContext = await MySqlServerRepository.OpenConnection())
            {
                var sql = @"INSERT INTO `user`(`role_id`, `name`, `gender`, `status`) 
                                VALUES (@RoleId, @Name, @Gender, @Status);
                                select last_insert_id();";

                request.Status = ConfigHelpers.STATUS_NEW;
                var result = (await dbContext.QueryAsync<int>(sql, request)).FirstOrDefault();

                var sqlCreateLogin = @"INSERT INTO `login`(`email`, `password`, `user_id`) 
                            VALUES (@Email, @Password, @UserID);
                            select last_insert_id()";

                var login = new LoginDto();
                login.Email = request.Email;
                //login.Password = 
                //var createLoginResult = (await dbContext.QueryAsync<int>)

                return result;
            }
        }

        public Task<UserDetail> GetUser(UserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
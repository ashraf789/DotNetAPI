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
                var sqlCheckLoginExist = @"SELECT count(*) FROM login where email = @Email";
                int user = (await dbContext.QueryAsync<int>(sqlCheckLoginExist, new { @Email = request.Email })).FirstOrDefault();

                if(user > 0)
                {
                    return ErrorConstants.USER_ALREADY_EXIST;
                }

                var sql = @"INSERT INTO `user`(`role_id`, `name`, `gender`, `status`) 
                                VALUES (@RoleId, @Name, @Gender, @Status);
                                select last_insert_id();";

                request.Status = ConfigHelpers.STATUS_NEW;
                var result = (await dbContext.QueryAsync<int>(sql, request)).FirstOrDefault();

                if(result < 1)
                {
                    return ErrorConstants.DB_NOT_FOUND_ERROR;
                }

                var sqlCreateLogin = @"INSERT INTO `login`(`email`, `password`, `user_id`) 
                            VALUES (@Email, @Password, @UserID);
                            select last_insert_id()";

                var login = new LoginDto();
                login.Email = request.Email;
                login.Password = ConfigHelpers.EncryptStringMD5(request.Password);
                login.UserID = result;

                await dbContext.QueryAsync<int>(sqlCreateLogin, login);

                return result;
            }
        }

        public async Task<UserDetail> GetUser(int userID)
        {
            using (var dbContext = await MySqlServerRepository.OpenConnection())
            {
                var sql = @"SELECT user.*, role.name as rolename, role.description as roledesctiption, role.prefix as roleprefix FROM user 
                            JOIN role on user.role_id = role.id 
                            WHERE user.id = @UserID";

                var result = (await dbContext.QueryAsync<UserDetail>(sql, new { @UserID = userID })).FirstOrDefault();
                return result;
            }
        }
        
        public async Task<UserDetail> GetUser(UserRequest request)
        {
            using (var dbContext = await MySqlServerRepository.OpenConnection())
            {
                var sql = @"SELECT user.*, login.*, role.name as rolename, role.description as roledesctiption, role.prefix as roleprefix FROM user 
                            JOIN role on user.role_id = role.id 
                            JOIN login on user.id = login.user_id 
                            WHERE login.email = @Email";

                var result = (await dbContext.QueryAsync<UserDetail>(sql, new { @Email = request.Email})).FirstOrDefault();
                return result;
            }
        }

    }
}
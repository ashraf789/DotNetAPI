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

                var sql = @"INSERT INTO `user`(`roleid`, `name`, `gender`, `status`) 
                                VALUES (@RoleId, @Name, @Gender, @Status);
                                select last_insert_id();";

                request.Status = ConfigHelpers.STATUS_NEW;
                var result = (await dbContext.QueryAsync<int>(sql, request)).FirstOrDefault();

                if(result < 1)
                {
                    return ErrorConstants.DB_NOT_FOUND_ERROR;
                }

                var sqlCreateLogin = @"INSERT INTO `login`(`email`, `password`, `userid`) 
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

        public async Task<UserDetail> GetUser(UserRequest request)
        {
            using (var dbContext = await MySqlServerRepository.OpenConnection())
            {
                var sql = @"SELECT user.*, login.*, role.name as rolename, role.description as roledescription, role.prefix as roleprefix FROM user 
                            JOIN role on user.roleid = role.id 
                            JOIN login on user.id = login.userid 
                            WHERE login.email = @Email";

                var result = (await dbContext.QueryAsync<UserDetail>(sql, new { @Email = request.Email})).FirstOrDefault();
                return result;
            }
        }

        public async Task<UserDto> GetUserInfo(UserInformationRequest request)
        {
            using (var dbContext = await MySqlServerRepository.OpenConnection())
            {
                var sql = @"SELECT * FROM user 
                            WHERE user.id = @UserID";

                var result = (await dbContext.QueryAsync<UserDto>(sql, new { @UserID = request.UserID })).FirstOrDefault();
                return result;
            }
        }
    }
}
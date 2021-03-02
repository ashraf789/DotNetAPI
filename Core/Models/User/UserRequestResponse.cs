using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Core.Models.User
{
    public class UserRequestResponse
    {
    }

    public class UserRequest
    {
        public string Email { get; set; }
    }

    public class CreateUserRequest
    {
        public int RoleID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
    }

    public class UserDetail
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DataType CreatedAt { get; set; }
        public int Status { get; set; }
        public RoleDto Role { get; set; }
    }
}
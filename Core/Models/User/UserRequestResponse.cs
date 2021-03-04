using Newtonsoft.Json;
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
        public string Password { get; set; }
    }

    public class UserInformationRequest
    {
        public string UserID { get; set; }
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
        [JsonProperty("user_id")]
        public int ID { get; set; }
        public int RoleID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        public string Gender { get; set; }
        [JsonIgnore]
        public int Status { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public string RolePrefix { get; set; }
    }
}
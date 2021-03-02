using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Core.Models
{
    public class UserDto
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DataType CreatedAt { get; set; }
        public int Status { get; set; }
    }
}
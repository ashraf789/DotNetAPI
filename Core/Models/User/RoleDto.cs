using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Models.User
{
    public class RoleDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Prefix { get; set; }
        public int Status { get; set; }
    }
}
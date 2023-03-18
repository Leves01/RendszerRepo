using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Models
{
    public class User
    {
        public int userId { get; set; }
        public string username { get; set; } = "default";
        public string password { get; set; } = "default123";
        public Roles userRole { get; set; } = Roles.techincian;
    }
}
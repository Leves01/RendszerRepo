using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Dtos.User
{
    public class AddUserDto
    {
        public string username { get; set; } = "default";
        public string password { get; set; } = "default123";
        public Roles userRole { get; set; } = Roles.techincian;
    }
}
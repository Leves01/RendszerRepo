using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Dtos.User
{
    public class GetUserDto
    {
        public int userId { get; set; }
        public string username { get; set; } = "default";
        public string password { get; set; } = "default123";
        public string userRole { get; set; } = "Technician";
    }
}
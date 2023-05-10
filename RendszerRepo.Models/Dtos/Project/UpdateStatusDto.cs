using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendszerRepo.Models.Dtos.Project
{
    public class UpdateStatusDto
    {
        public int projectId { get; set; }
        public string Status { get; set; }
    }
}

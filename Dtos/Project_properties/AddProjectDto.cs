using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Dtos.Project_properties
{
    public class AddProjectDto
    {
        public string ProjectName { get; set; }
        public string Status { get; set; }
        public string Location{get; set;}
        public string CustomerName{get; set;}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Dtos.Project_properties
{
    public class GetProject_propertiesDto
    {
        public int projectId {get; set;}
        public string ProjectName { get; set; }
        public string Status { get; set; }
        [Display(Name = "AssignedWorker")]
        public int assignedId {get; set;}
        [ForeignKey("UserId")] 
        public virtual GetUserDto? User { get; set; }
        public string Location {get; set;}
        public string CustomerName{get; set;}
    }
}
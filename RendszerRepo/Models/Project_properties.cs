using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Models
{
    [Keyless]
    public class Project_properties
    {
        [Display(Name = "Project")]
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")] 
        public virtual Project Project { get; set; }

        public string ProjectName { get; set; }

        public string Status { get; set; }

        [Display(Name = "AssignedWorker")]
        public int assignedId {get; set;}
        
        [ForeignKey("UserId")] 
        public virtual User User { get; set; }

    }
}
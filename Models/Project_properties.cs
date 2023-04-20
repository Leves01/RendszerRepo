using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Models
{
    public class Project_properties
    {
        [Key]
        public int ProjectId { get; set; }
       
        public string ProjectName { get; set; }

        public string Status { get; set; }

        [Display(Name = "AssignedWorker")]
        public int assignedId {get; set;}
        
        [ForeignKey("UserId")] 
        public virtual User User { get; set; }

        public string Location {get; set;}
        public string CustomerName{get; set;}

    }
}
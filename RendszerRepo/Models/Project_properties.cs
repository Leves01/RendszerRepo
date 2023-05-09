using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Models
{
    [Keyless]
    public class Project_properties
    {
        [Display(Name = "Projekt")]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")] 
        public virtual Project Project { get; set; }
        [Display(Name = "AssignedWorker")]
        public int assignedId {get; set;}
        [ForeignKey("UserId")] 
        public virtual User User { get; set; }
        [Display(Name = "Part")] 
        public int partId { get; set; }
        [ForeignKey("partId")] 
        public virtual Part Part {get; set;}
        public int quantity {get; set;}
        public int combinedPrice {get; set;}
        public int workPrice {get; set;}
        public int workTime {get; set;}
    }
}
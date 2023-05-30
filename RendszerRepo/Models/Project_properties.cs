using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Models
{
    public class Project_properties
    {
        [Key]
        public int OwnId {get; set;}
        [Display(Name = "Projekt")]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")] 
        public virtual Project Project { get; set; }
        [Display(Name = "AssignedWorker")]
        public int userId{get; set;} = 1;
        [ForeignKey("userId")] 
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Models
{
    public class Projects
    {
        public int ProjectId { get; set; }
        [Display(Name = "Part")] 
        public int partId { get; set; }
        [ForeignKey("partId")] 
        public virtual Part Parts { get; set; }
        public int quantity {get; set;}
        public int combinedPrice {get; set;}
    }
}

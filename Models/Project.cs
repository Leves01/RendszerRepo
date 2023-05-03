using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Models
{
    public class Project
    {
        [Key]
        public int OwnId { get; set; }
         [Display(Name = "Project")]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")] 
        public virtual Project_properties Project_p { get; set; }
        [Display(Name = "Part")] 
        public List<int> partId{ get; set; }
        [ForeignKey("partId")] 
        public int quantity { get; set; }
        public int combinedPrice { get; set; }
        public int workTime { get; set;}
        public int workPrice{ get; set; }
    }
}

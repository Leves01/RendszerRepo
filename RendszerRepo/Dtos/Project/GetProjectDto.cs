using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Dtos.Project
{
    public class GetProjectDto
    {
       
        public int ProjectId { get; set; }
        [Display(Name = "Part")] 
        public int partId { get; set; }
        [ForeignKey("partId")] 
        public virtual GetPartDto? Parts { get; set; }
        public int quantity { get; set; }
        public int combinedPrice { get; set; }
        public int workTime { get; set;}
        public int workPrice{ get; set; }
    }
}
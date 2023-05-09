using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Dtos.Project_properties
{
    public class AddProjectDto
    {
        [Display(Name = "AssignedWorker")]
        public int assignedId {get; set;}
        [Display(Name = "Part")] 
        public int partId { get; set; }
        public int quantity {get; set;}
        public int combinedPrice {get; set;}

    }
}
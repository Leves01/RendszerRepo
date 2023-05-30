using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Dtos.Project_properties
{
    public class AddProjectDto
    {
        [Display(Name = "Projekt")]
        public int ProjectId { get; set; }
        [Display(Name = "AssignedWorker")]
        public int userId {get; set;}
        [Display(Name = "Part")] 
        public int partId { get; set; }
        public int quantity {get; set;}
        public int combinedPrice {get; set;}
        public int workPrice {get; set;}
        public int workTime {get; set;}
    }
}
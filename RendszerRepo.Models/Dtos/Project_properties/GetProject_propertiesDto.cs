using RendszerRepo.Dtos.Part;
using RendszerRepo.Dtos.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Dtos.Project_properties
{
    public class GetProject_propertiesDto
    {
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")] 
        public virtual GetProject_propertiesDto? Project_p { get; set; }
        [Display(Name = "AssignedWorker")]
        public int assignedId {get; set;}
        [ForeignKey("UserId")] 
        public virtual GetUserDto? User { get; set; }
        [Display(Name = "Part")] 
        public int partId { get; set; }
        [ForeignKey("partId")] 
        public virtual GetPartDto? Parts { get; set; }
        public int quantity {get; set;}
        public int combinedPrice {get; set;}
    }
}
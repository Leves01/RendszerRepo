using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RendszerRepo.Dtos.Part;
using RendszerRepo.Dtos.Project;

namespace RendszerRepo.Models.Dtos.Reserves
{
    public class GetReserveDto
    {
        public int reservedPartsId { get; set; }
        public int partId { get; set; }
        [ForeignKey("partId")] 
        public virtual GetPartDto? part { get; set; }
        public int projectId { get; set; }
        public virtual GetProjectDto? project { get; set; }
        public int neededAmount {get; set;}
    }
}
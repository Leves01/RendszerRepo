using RendszerRepo.Dtos.Part;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Dtos.Project
{
    public class GetProjectDto
    {
       
        public int ProjectId { get; set; }
        public int partId { get; set; }
        public int quantity { get; set; }
        public int combinedPrice { get; set; }
        public int workTime { get; set;}
        public int workPrice{ get; set; }
    }
}
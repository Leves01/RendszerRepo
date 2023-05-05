using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Dtos.Part
{
    public class PartToProjectDto
    {
         public int partId { get; set; }
         public int ProjectId { get; set; }
         public int quantity{ get; set; }
    }
}
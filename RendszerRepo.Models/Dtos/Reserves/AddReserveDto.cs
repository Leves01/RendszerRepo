using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Models.Dtos.Reserves
{
    public class AddReserveDto
    {
        // public int partId { get; set; }
        public int projectId { get; set; }
        public int partId { get; set; }
        public int neededAmount {get; set;}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Models.Dtos.Reserves
{
    public class UpdateReserveDto
    {
        public int reservedPartsId { get; set; }
        public int neededAmount {get; set;}
    }
}
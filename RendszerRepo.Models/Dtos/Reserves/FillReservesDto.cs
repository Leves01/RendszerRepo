using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendszerRepo.Models.Dtos.Reserves
{
    public class FillReservesDto
    {
        public int ReservedId { get; set; }
        public int PartId { get; set; }
        public int ProjectId { get; set; }
    }
}

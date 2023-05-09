using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Dtos.Part
{
    public class UpdatePartPriceDto
    {
        public int partId { get; set; }
        public int price { get; set; }
    }
}
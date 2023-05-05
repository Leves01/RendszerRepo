using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Dtos.Part
{
    public class AddPartDto
    {
        public string partName { get; set; } = "napelem";
        public int price { get; set; }
        public int maxCount { get; set; }
    }
}
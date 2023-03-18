using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Models
{
    public class Part
    {
        public int partId { get; set; }
        public string? partName { get; set; }
        public int currentStatus { get; set; }
        public int price { get; set; }
    }
}
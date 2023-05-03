using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Dtos.Project
{
    public class AddPrDto
    {
        public string ProjectName { get; set; }
        public string Status { get; set; }
        public string Location{get; set;}
        public string CustomerName{get; set;}
    }
}
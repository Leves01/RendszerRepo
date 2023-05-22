using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Models
{
    public class reservedParts
    {
        public int reservedPartsId { get; set; }
        public int partId { get; set; }
        [ForeignKey("partId")] 
        public virtual Part part { get; set; }
        public int projectId {get; set;}
        [ForeignKey("projectId")]
        public virtual Project project {get; set;}
        public int neededAmount {get; set;}
    }
}
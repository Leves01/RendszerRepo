using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Dtos.Storage
{
    public class UpdateStoragesDto
    {
        public int storageId { get; set; }

        [Display(Name = "Part")] 
        public int partId { get; set; }

        [ForeignKey("partId")] 
        public virtual GetPartDto? Parts { get; set; }
        
        public int row { get; set; }
        public int column { get; set; }
        public int drawer { get; set; }
        public int countOfParts { get; set; }
        public int max {get; set;}
    }
}
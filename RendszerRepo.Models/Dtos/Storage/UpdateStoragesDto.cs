using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Dtos.Storage
{
    public class UpdateStoragesDto
    {
        public int storageId { get; set; }

        [Display(Name = "Part")] 
        public int partId { get; set; }   
        public int countOfParts { get; set; }
        public int max {get; set;}
    }
}
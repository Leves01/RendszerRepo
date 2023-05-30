using RendszerRepo.Dtos.Part;
using RendszerRepo.Dtos.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Dtos.Project_properties
{
    public class GetProject_propertiesDto
    {
        public int ProjectId { get; set; }
        public int userId {get; set;}
        public int partId { get; set; }
        public int quantity {get; set;}
        public int combinedPrice {get; set;}
    }
}
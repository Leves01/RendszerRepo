using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Dtos.Storage
{
    public class UpdatePartCountDto
    {
        public int storageId { get; set; }

        public int countOfParts { get; set; }
    }
}
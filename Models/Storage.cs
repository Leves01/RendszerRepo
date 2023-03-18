using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Models
{
    public class Storage
    {
        public int storageId { get; set; }
        public int partId { get; set; }
        public int row { get; set; }
        public int column { get; set; }
        public int drawer { get; set; }
        public int countOfParts { get; set; }
    }
}
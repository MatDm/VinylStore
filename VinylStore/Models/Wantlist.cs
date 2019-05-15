using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStore.Models
{
    public class Wantlist
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string VinylId { get; set; }

        //public bool IsPossessed { get; set; }
    }
}

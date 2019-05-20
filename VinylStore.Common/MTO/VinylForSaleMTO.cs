using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStore.Common.MTO
{
    public class VinylForSaleMTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        //REFACTOR A DISPARAITRE
        public string VinylId { get; set; }

        public VinylMTO Vinyl { get; set; }

        //public bool IsPossessed { get; set; }
    }
}

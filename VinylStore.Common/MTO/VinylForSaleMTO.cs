using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Common.Auth;

namespace VinylStore.Common.MTO
{
    public class VinylForSaleMTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        //REFACTOR A DISPARAITRE
        public string VinylId { get; set; }

        public virtual VinylMTO Vinyl { get; set; }

        //public bool IsPossessed { get; set; }
    }
}

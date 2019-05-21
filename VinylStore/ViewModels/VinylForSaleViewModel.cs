using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Common.Auth;
using VinylStore.Common.MTO;
using VinylStore.DAL.Entities;

namespace VinylStore.ViewModels
{
    public class VinylForSaleViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }        
        public virtual ApplicationUser User { get; set; }
        public string VinylId { get; set; }        
        public virtual VinylViewModel Vinyl { get; set; }
        //public bool IsPossessed { get; set; }
    }
}

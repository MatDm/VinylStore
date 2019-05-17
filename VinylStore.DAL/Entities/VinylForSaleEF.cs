using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStore.DAL.Entities
{
    [Table("Collections")]
    public class VinylForSaleEF
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string VinylId { get; set; }
        //public bool IsPossessed { get; set; }

    }
}

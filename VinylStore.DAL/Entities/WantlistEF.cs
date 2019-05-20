using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Common.Auth;

namespace VinylStore.DAL.Entities
{
    [Table("Wantlists")]
    public class WantlistEF
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public string VinylId { get; set; }

        [ForeignKey("VinylId")]
        public virtual VinylEF Vinyl { get; set; }

        public DateTime Validity { get; set; }
        public Double MaxPrice { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStore.ViewModels
{
    public class VinylShortViewModel
    {
        public string ImageUrl { get; set; }
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }
        public decimal Price { get; set; }
        public string VinylId { get; set; }
    }
}

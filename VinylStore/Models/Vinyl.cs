using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStore.Entities
{
    public class Vinyl
    {
        public int Id { get; set; }
        public string AlbumName { get; set; }
        public string BandName { get; set; }
        public string Genre { get; set; }
        public string ReleaseYear { get; set; }
        public decimal Price { get; set; }
    }
}

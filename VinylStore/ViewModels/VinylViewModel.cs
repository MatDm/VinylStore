using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStore.ViewModels
{
    public class VinylViewModel
    {
        public string ImageUrl { get; set; }
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }
        public string[] Genres { get; set; }
        public string ReleaseYear { get; set; }
        public string[] TrackList { get; set; }
        public decimal Price { get; set; }
    }
}

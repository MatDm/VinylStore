using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStore.Models
{
    public class VinylModel
    {                
        public string Id { get; set; }
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }       
        public string ReleaseYear { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public string SpotifyAlbumId { get; set; }        
        public string[] TrackList { get; set; }       
        public string[] Genres { get; set; }
        public decimal Price { get; set; }      
    }
}

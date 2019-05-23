using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStore.ViewModels
{
    public class VinylViewModel
    {
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Please enter an album name")]
        [StringLength(100)]
        //[Display(Name ="Album name")]
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }
        [Required(ErrorMessage = "Please enter one or more genre")]
        [StringLength(100)]
        public string Genres { get; set; }
        public string ReleaseYear { get; set; }
        public string TrackList { get; set; }
        [Required(ErrorMessage = "Please enter a price")]
        [Range(1, 1000000, ErrorMessage = "Please enter a valid price")]
        public decimal Price { get; set; }
        public string Id { get; set; }
        public string SpotifyAlbumId { get; set; }
        [Required(ErrorMessage = "Please enter a description : general state of the vinyl, cover, any particularity...")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Description is too short/long")]
        public string Description { get; set; }
        public string  Label { get; set; }
    }
}

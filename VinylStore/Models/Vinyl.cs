using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStore.Models
{
    public class Vinyl
    {
        [Column("TrackList")]
        public string _trackList { get; set; }
        [Column("Genres")]
        public string _genres { get; set; }
        private static readonly char trackDelimiter = ';';
        private static readonly char genreDelimiter = '/';

        public string Id { get; set; }
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }       
        public string ReleaseYear { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public string SpotifyAlbumId { get; set; }
        [NotMapped]
        public string[] TrackList
        {
            get { return _trackList.Split(trackDelimiter); }
            set
            {
                _trackList = string.Join($"{trackDelimiter}", value);
            }
        }
        [NotMapped]
        public string[] Genres
        {
            get { return _genres.Split(genreDelimiter); }
            set
            {
                _genres = string.Join($"{genreDelimiter}", value);
            }
        }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }      
    }
}

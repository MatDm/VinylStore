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

        private static readonly char delimiter = ';';
        public int Id { get; set; }
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }
        public string Genre { get; set; }
        public string ReleaseYear { get; set; }
        public string ImageUrl { get; set; }

        [NotMapped]
        public string[] TrackList
        {
            get { return _trackList.Split(delimiter); }
            set
            {
                _trackList = string.Join($"{delimiter}", value);
            }
        }
        public decimal Price { get; set; }
    }
}

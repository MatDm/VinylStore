using System;
using System.Collections.Generic;
using System.Text;
using VinylStore.Common.MTO;
using VinylStore.DAL.Entities;

namespace VinylStore.DAL.Extensions
{
    public static class VinylExtensions
    {

        public static VinylMTO ToMTO(this VinylEF vinylEntity)
            => new VinylMTO()
            {
                AlbumName = vinylEntity.AlbumName,
                ArtistName = vinylEntity.ArtistName,
                ImageUrl = vinylEntity.ImageUrl,
                Genres = vinylEntity.Genres,
                Label = vinylEntity.Label,
                Description = vinylEntity.Description,
                Price = vinylEntity.Price,
                Id = vinylEntity.Id,
                ReleaseYear = vinylEntity.ReleaseYear,
                SpotifyAlbumId = vinylEntity.ReleaseYear,
                TrackList = vinylEntity.TrackList

            };

        public static VinylEF ToEntity(this VinylMTO vinylMTO)
            => new VinylEF()
            {
                AlbumName = vinylMTO.AlbumName,
                ArtistName = vinylMTO.ArtistName,
                ImageUrl = vinylMTO.ImageUrl,
                Genres = vinylMTO.Genres,
                Label = vinylMTO.Label,
                Description = vinylMTO.Description,
                Price = vinylMTO.Price,
                Id = vinylMTO.Id,
                ReleaseYear = vinylMTO.ReleaseYear,
                SpotifyAlbumId = vinylMTO.ReleaseYear,
                TrackList = vinylMTO.TrackList

            };
    }
}

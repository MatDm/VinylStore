using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Common.MTO;

namespace VinylStore.DAL.ExternalServices.JsonModels.TypeExtentions
{
    public static class AlbumIdSearchResultJsonModelExtension
    {
        public static VinylMTO ToMTO(this AlbumIdSearchResultJsonModel album)
        {
            VinylMTO vinyl = new VinylMTO()
            {
                AlbumName = album.name,
                ReleaseYear = album.release_date,
                ArtistName = album.artists[0].name,
                ImageUrl = album.images[0].url,
                Label = album.label
            };
            return vinyl;
        }
    }
}

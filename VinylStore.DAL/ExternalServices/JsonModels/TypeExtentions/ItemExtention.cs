using VinylStore.Common.MTO;

namespace VinylStore.DAL.ExternalServices.JsonModels
{
    public static class ItemExtention
    {
        public static VinylMTO ToMTO(this Item item)
            => new VinylMTO
            {
                 AlbumName = item.name,
                 ArtistName = item.artists[0].name,
                 ImageUrl = item.images[0].url,
                 SpotifyAlbumId =item.id
            };

        //private static string ArrayToArtistString(this Artist artistArray)
        //{

        //}
    }

}


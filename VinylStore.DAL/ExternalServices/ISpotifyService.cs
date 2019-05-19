using System.Collections.Generic;
using VinylStore.Common.MTO;

namespace VinylStore.DAL.ExternalServices
{
    public interface ISpotifyService
    {
        //string[] GetArtistGenres(string ArtistName);
        List<VinylMTO> GetVinyls(string query, string artistName = "", string year = "", string genre = "", string upc = "", string isrc = "", int limit = 20, int offset = 0);
        VinylMTO GetVinylDetails(string spotifyAlbumId);
    }
}
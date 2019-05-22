using System.Threading.Tasks;
using VinylStore.DAL.ExternalServices.JsonModels;

namespace VinylStore.DAL.ExternalServices
{
    public interface ISpotifyProxy
    {
        string GetTracks(AlbumIdSearchResultJsonModel result);
        Task<string> RefreshToken();
        Task<string> GetGenres(AlbumIdSearchResultJsonModel result);
        Task<AlbumIdSearchResultJsonModel> GetAlbumById(string spotifyAlbumId);
        Task<AlbumSearchResultJsonModel> GetAlbum(string query, string artistName = "",
            string year = "",
            string genre = "",
            string upc = "",
            string isrc = "",
            int limit = 20,
            int offset = 0);
    }
}
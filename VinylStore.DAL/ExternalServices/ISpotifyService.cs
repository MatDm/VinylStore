using System.Threading.Tasks;
using VinylStore.DAL.ExternalServices.JsonModels;

namespace VinylStore.DAL.ExternalServices
{
    public interface ISpotifyService
    {
        string[] GetTracks(AlbumIdSearchResultJsonModel result);
        Task<string> RefreshToken();
        Task<string[]> GetGenres(AlbumIdSearchResultJsonModel result);
    }
}
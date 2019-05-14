using System.Threading.Tasks;
using VinylStore.JsonModels;

namespace VinylStore.Abstract
{
    public interface ISpotifyService
    {
        string[] GetTracks(AlbumIdSearchResultJsonModel result);
        Task<string> RefreshToken();
        Task<string[]> GetGenres(AlbumIdSearchResultJsonModel result);
    }
}
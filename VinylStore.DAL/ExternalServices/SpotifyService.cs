using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylStore.Common.MTO;
using VinylStore.DAL.ExternalServices.JsonModels;
using VinylStore.DAL.ExternalServices.JsonModels.TypeExtentions;

namespace VinylStore.DAL.ExternalServices
{
    public class SpotifyService : ISpotifyService
    {
        SpotifyProxy spotifyProxy = new SpotifyProxy();

        public SpotifyService(/* !!!si necessaire!!! ajouter injection ici*/)
        {
            //spotifyProxy = new SpotifyProxy();
        }

        public VinylMTO GetVinylDetails(string spotifyAlbumId) { 


            var album = spotifyProxy.GetAlbumById(spotifyAlbumId).Result;

            //on vérifie si c'est pas vide
            if (album != null)
            {
                //on transforme le json en vinylMTO
                VinylMTO vinylMTO = album.ToMTO();

                vinylMTO.SpotifyAlbumId = spotifyAlbumId;
                vinylMTO.TrackList = spotifyProxy.GetTracks(album);

                var taskThread = Task.Run(async () => vinylMTO.Genres = await spotifyProxy.GetGenres(album));

                taskThread.Wait();

                return vinylMTO;

            }

            return null;
        }

        public List<VinylMTO> GetVinyls(string query, string artistName = "",
            string year = "",
            string genre = "",
            string upc = "",
            string isrc = "",
            int limit = 20,
            int offset = 0)
        {
            AlbumSearchResultJsonModel albumsSearch = new AlbumSearchResultJsonModel();
            
            var TaskThread = Task.Run(async () => albumsSearch = await spotifyProxy.GetAlbum(query, artistName, year, genre, upc, isrc, limit, offset));

            TaskThread.Wait();

            return albumsSearch.albums.items.Select( x=> x.ToMTO()).ToList();

        }
    }
}

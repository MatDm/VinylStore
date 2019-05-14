using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VinylStore.JsonModels;
using VinylStore.Models;

namespace VinylStore.Abstract
{
    public class SpotifyService : ISpotifyService
    {
        public async Task<string> RefreshToken()
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                string urlToken = "https://accounts.spotify.com/api/token";
                request.Headers.Add("Authorization", "Basic MzM4YzVlZDI1NWEwNDA5NGEyNWIyZGFjMGZkNjYzMzU6ZGUzNDUwYWE3Y2JlNGVlNjgzOGQ1ZDhlODYyMDAyY2Q=");
                var body = new List<KeyValuePair<string, string>>();
                body.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                request.Content = new FormUrlEncodedContent(body);
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(urlToken);
                var result = await client.SendAsync(request);
                var accessTokenObject = await result.Content.ReadAsAsync<AccessTokenJsonModel>();
                return accessTokenObject.access_token;
            }
        }

        public string[] GetTracks(AlbumIdSearchResultJsonModel result)
        {
            var tracksLength = result.tracks.items.Length;

            //si result contient un ou plusieurs tracks
            if (tracksLength > 0)
            {
                var tracks = new string[tracksLength];

                for (int i = 0; i < tracksLength; i++)
                {
                    tracks[i] = result.tracks.items[i].name;
                }
                return tracks;
            }

            //Si pas de genres
            return null;
        }

        /// <summary>
        /// on récupère les genres contenu dans le json model de spotify
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public async Task<string[]> GetGenres(AlbumIdSearchResultJsonModel result)
        {
            //requete par artiste vers spotify pour choper les genres de l'artiste présent dans result
            string artistName = result.artists[0].name;
            string url = $"https://api.spotify.com/v1/search?q={artistName}&type=artist";

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await RefreshToken());
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(url);
                var output = await client.SendAsync(request);

                //on récupère le json de spotify
                var apiResult = await output.Content.ReadAsAsync<ArtistSearchResultJsonModel>();

                return apiResult.artists.items[0].genres;
            }           
        }
    }
}

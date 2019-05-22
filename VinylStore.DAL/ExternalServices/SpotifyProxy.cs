using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VinylStore.DAL.ExternalServices.JsonModels;

namespace VinylStore.DAL.ExternalServices
{
    public class SpotifyProxy : ISpotifyProxy
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

        public string GetTracks(AlbumIdSearchResultJsonModel album) //REFACTOR rename to TrackRequest
        {
            var tracksLength = album.tracks.items.Length;

            //si result contient un ou plusieurs tracks
            if (tracksLength > 0)
            {
                var tracksArray = new string[tracksLength];

                for (int i = 0; i < tracksLength; i++)
                {
                    tracksArray[i] = album.tracks.items[i].name;
                }
                var tracksDelimiter = "\t/\t";
                var trackString = string.Join($"{tracksDelimiter}", tracksArray);
                return trackString;
            }
            //Si pas de tracks
            return null;
        }

        /// <summary>
        /// on récupère les genres contenu dans le json model de spotify
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public async Task<string> GetGenres(AlbumIdSearchResultJsonModel album)
        {
            //requete par artiste vers spotify pour choper les genres de l'artiste présent dans result
            string artistName = album.artists[0].name;
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
                //définit un délimiteur pour la future string
                var genreDelimiter = "\t/\t";
                //création de la string des genres à partir de l'array obtenue(json)
                var genres = string.Join($"{genreDelimiter}", apiResult.artists.items[0].genres);

                return genres;
            }
        }

        public async Task<AlbumIdSearchResultJsonModel> GetAlbumById(string spotifyAlbumId)
        {
            string queryString = "https://api.spotify.com/v1/albums/" + spotifyAlbumId;

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await RefreshToken());
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(queryString);
                var output = await client.SendAsync(request);

                //on récupère le json de spotify
                return await output.Content.ReadAsAsync<AlbumIdSearchResultJsonModel>();

            }
        }

        public async Task<AlbumSearchResultJsonModel> GetAlbum(string query, string artistName = "",
            string year = "",
            string genre = "",
            string upc = "",
            string isrc = "",
            int limit = 20,
            int offset = 0)
        {
            var refreshedToken = await RefreshToken();

            string queryString = "https://api.spotify.com/v1/search?q=album:" + query.Replace(" ", "%20");

            if (artistName != "")
                queryString += "%20:artist:" + artistName.Replace(" ", "%20");
            if (year != "")
                queryString += "%20:year:" + year.Replace(" ", "%20");
            if (genre != "")
                queryString += "%20:genre:" + genre.Replace(" ", "%20");
            if (upc != "")
                queryString += "%20:upc:" + upc.Replace(" ", "%20");
            if (isrc != "")
                queryString += "%20:isrc:" + isrc.Replace(" ", "%20");

            queryString += "&limit=" + limit;
            queryString += "&offset=" + offset;
            queryString += "&type=album";

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", refreshedToken);
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(queryString);
                var result = await client.SendAsync(request);
                //tester statuscode
                return await result.Content.ReadAsAsync<AlbumSearchResultJsonModel>();
            }
        }
    }
}

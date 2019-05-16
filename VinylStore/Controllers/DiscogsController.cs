using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Common.Contracts;
using VinylStore.DAL.ExternalServices;
using VinylStore.DAL.ExternalServices.JsonModels;

namespace VinylStore.Controllers
{
    public class DiscogsController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISpotifyService _spotifyService;
        private string consumerKey = "QvviaqTYLJDSiYtyXjbE";
        private string consumerSecret = "NajYtMXVBZnrYocbXRsCbWinUCPXgMXI";

        public DiscogsController(IUserService userService, ISpotifyService spotifyService)
        {
            _userService = userService;
            _spotifyService = spotifyService;
        }
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string query, string artistName = "",
            string year = "",
            string genre = "",
            string upc = "",
            string isrc = "",
            int limit = 20,
            int offset = 0)
        {
            //1ere requete pour rafraichir le token d'accès

            var refreshedToken = await _spotifyService.RefreshToken();

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
                var parsedResult = await result.Content.ReadAsAsync<AlbumSearchResultJsonModel>();
                return View("SearchResult", parsedResult);

            }

            //2eme requete : après avoir récupérer la releaseId, 
            //on l'utilise pour récupérer les données à enregistrer (dont la tracklist!!)
            //string urlWithReleaseId
        }

        
    }
}
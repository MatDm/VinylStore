using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Models;

namespace VinylStore.Controllers
{
    public class DiscogsController : Controller
    {
        private string consumerKey = "QvviaqTYLJDSiYtyXjbE";
        private string consumerSecret = "NajYtMXVBZnrYocbXRsCbWinUCPXgMXI";

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string query)
        {
            ////1ere requete par le input du user
            //string urlUserInput = "https://api.discogs.com/database/search?q="
            //    + query + "&key=" + consumerKey + "&secret=" + consumerSecret;
            //using (var client = new HttpClient())
            //using (var request = new HttpRequestMessage())
            //{
            //    request.Headers.Add("User-Agent", "VinylStore");
            //    request.Method = HttpMethod.Get;
            //    request.RequestUri = new Uri(urlUserInput);
            //    var result = await client.SendAsync(request);
            //    //tester statuscode
            //    var parsedResult = await result.Content.ReadAsAsync<SearchVinyl>();
            //    //return View("SearchResult", parsedResult);
            //    parsedResult.results.
            //}

            ////2eme requete : après avoir récupérer la releaseId, 
            ////on l'utilise pour récupérer les données à enregistrer (dont la tracklist!!)
            //string urlWithReleaseId

            var output = await SpotifyWebAPI.Artist.GetArtist(query);
            return View("SearchResult", output);
        }
    }
}
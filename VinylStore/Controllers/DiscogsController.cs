using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotifyWebAPI;
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

            string queryString = "https://api.spotify.com/v1/search?q=album:" + query.Replace(" ", "%20");

            //if (artistName != "")
            //    queryString += "%20:artist:" + artistName.Replace(" ", "%20");
            //if (year != "")
            //    queryString += "%20:year:" + year.Replace(" ", "%20");
            //if (genre != "")
            //    queryString += "%20:genre:" + genre.Replace(" ", "%20");
            //if (upc != "")
            //    queryString += "%20:upc:" + upc.Replace(" ", "%20");
            //if (isrc != "")
            //    queryString += "%20:isrc:" + isrc.Replace(" ", "%20");

            //queryString += "&limit=" + limit;
            //queryString += "&offset=" + offset;
            queryString += "&type=album";

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Headers.Add("Authorization", "Bearer BQCT0A6vl8bjfRH13hugiaYdUaQeVw4ksPqZKwPaQwLaBbV9JA7J31PvcCpnvgLOzOalCZv4W-vi3Hbyx7A");
                 
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(queryString);
                var result = await client.SendAsync(request);
                //tester statuscode
                var parsedResult = await result.Content.ReadAsAsync<SearchVinyl2>();
                return View("SearchResult", parsedResult);
                
            }

            //var output = await SpotifyWebAPI.Artist.GetArtist(queryString);
            //return View("SearchResult", output);
        }
    }
}
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
        
        private readonly ISpotifyService _spotifyService;
        private string consumerKey = "QvviaqTYLJDSiYtyXjbE";
        private string consumerSecret = "NajYtMXVBZnrYocbXRsCbWinUCPXgMXI";

        public DiscogsController(ISpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchByAlbum(string query, string artistName = "",
            string year = "",
            string genre = "",
            string upc = "",
            string isrc = "",
            int limit = 20,
            int offset = 0)
        {
            var album = _spotifyService.GetVinyls(query, artistName, year, genre, upc, isrc, limit, offset);


            return View("SearchResult", album);
        }
        //2eme requete : après avoir récupérer la releaseId, 
        //on l'utilise pour récupérer les données à enregistrer (dont la tracklist!!)
        //string urlWithReleaseId
    }
}

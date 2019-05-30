using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VinylStore.BLL.UseCases;
using VinylStore.Common.Auth;
using VinylStore.Common.Contracts;
using VinylStore.DAL.ExternalServices;
using VinylStore.DAL.ExternalServices.JsonModels;
using VinylStore.ViewModels.TypeExtentions;

namespace VinylStore.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly IVinylRepository _vinylRepo;
        private readonly Func<string, IListRepository> _listRepositoryAccessor;
        private readonly ISpotifyService _spotifyService;
        private readonly UserManager<ApplicationUser> _userManager;
        //private string consumerKey = "QvviaqTYLJDSiYtyXjbE";
        //private string consumerSecret = "NajYtMXVBZnrYocbXRsCbWinUCPXgMXI";

        public SearchController(UserManager<ApplicationUser> userManager,
            IVinylRepository vinylRepo, Func<string, IListRepository> listRepositoryAccessor, ISpotifyService spotifyService)
        {
            _userManager = userManager;
            _vinylRepo = vinylRepo;
            _listRepositoryAccessor = listRepositoryAccessor;
            _spotifyService = spotifyService;
        }

        [HttpGet]
        public IActionResult SearchByAlbum()
        {
            return RedirectToAction("Index", "Home");
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
            var UserRole = new UserUC(User.FindFirst(ClaimTypes.NameIdentifier).Value, _vinylRepo, _listRepositoryAccessor, _spotifyService);
            var album = UserRole.SearchByAlbum(query, artistName, year, genre, upc, isrc, limit, offset);

            return View("SearchResult", album);
        }
        //2eme requete : après avoir récupérer la releaseId, 
        //on l'utilise pour récupérer les données à enregistrer (dont la tracklist!!)
        //string urlWithReleaseId
    }
}

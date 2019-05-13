using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Abstract;
using VinylStore.Auth;
using VinylStore.Concrete;
using VinylStore.JsonModels;
using VinylStore.Models;
using VinylStore.Services;
using VinylStore.ViewModels;

namespace VinylStore.Controllers
{
    [Authorize]
    public class WantlistController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IVinylRepository _vinylRepo;
        private readonly IUserVinylRepository _userVinylRepo;
        private readonly IUserService _userService;

        public WantlistController(UserManager<ApplicationUser> userManager, IVinylRepository vinylRepo, IUserVinylRepository userVinylRepo, IUserService userService)
        {
            _userManager = userManager;
            _vinylRepo = vinylRepo;
            _userVinylRepo = userVinylRepo;
            _userService = userService;
        }

        public async Task<IActionResult> DisplayMyWantlist()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var vinylWantlist = _userService.GetMyWantlist(currentUser.Id);
            var vinylShortViewModelList = new List<VinylShortViewModel>();
            foreach (var vinyl in vinylWantlist)
            {
                var shortModel = new VinylShortViewModel();

                shortModel.ImageUrl = vinyl.ImageUrl;
                shortModel.AlbumName = vinyl.AlbumName;
                shortModel.ArtistName = vinyl.ArtistName ?? "";

                vinylShortViewModelList.Add(shortModel);
            }
            return View(vinylShortViewModelList);
        }
        public async Task<IActionResult> AddToUserWantlist(string spotifyAlbumId)
        {
            //requete par id de l'album pour avoir les données complètes à sauver dans la table
            string queryString = "https://api.spotify.com/v1/albums/" + spotifyAlbumId;

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _userService.RefreshToken());
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(queryString);
                var output = await client.SendAsync(request);
                //tester statuscode
                var result = await output.Content.ReadAsAsync<AlbumIdSearchResultJsonModel>();
                Vinyl vinyl = new Vinyl()
                {
                    AlbumName = result.name,
                    ReleaseYear = result.release_date,
                    ArtistName = result.artists[0].name,
                    ImageUrl = result.images[0].url,
                    Label = result.label,
                    SpotifyAlbumId = spotifyAlbumId
                };
                _vinylRepo.Insert(vinyl);
                var currentUser = await _userManager.GetUserAsync(User);
                var userVinyl = new UserVinyl() { UserId = currentUser.Id, VinylId = vinyl.Id, IsPossessed = false };
                _userVinylRepo.Insert(userVinyl);
                return RedirectToAction("DisplayMyCollection");
            }
        }
        public IActionResult Delete(int vinylId)
        {
            if (_vinylRepo.Delete(vinylId) == true)
            {
                TempData["SuccessMessage"] = "Vinyl deleted";
                return RedirectToAction("DisplayMyCollection");
            }
            TempData["ErrorMessage"] = "Vinyl not deleted, something went wrong";
            return RedirectToAction("DisplayMyCollection");
        }
    }
}
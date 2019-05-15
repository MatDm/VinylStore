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
using VinylStore.Abstract;
using VinylStore.JsonModels;
using VinylStore.Models;
using VinylStore.Abstract;
using VinylStore.ViewModels;

namespace VinylStore.Controllers
{
    [Authorize]
    public class WantlistController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IVinylRepository _vinylRepo;
        private readonly Func<string, IListRepository> _listRepositoryAccessor;
        private readonly IUserService _userService;
        private readonly ISpotifyService _spotifyService;

        public WantlistController(UserManager<ApplicationUser> userManager, IVinylRepository vinylRepo, 
            Func<string, IListRepository> listRepositoryAccessor, IUserService userService, ISpotifyService spotifyService)
        {
            _userManager = userManager;
            _vinylRepo = vinylRepo;
            _listRepositoryAccessor = listRepositoryAccessor;
            _userService = userService;
            _spotifyService = spotifyService;
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
                shortModel.VinylId = vinyl.Id;

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

                //on récupère le json de spotify
                var result = await output.Content.ReadAsAsync<AlbumIdSearchResultJsonModel>();

                //on vérifie si c'est pas vide
                if (result != null)
                {
                    Vinyl vinyl = new Vinyl()
                    {
                        AlbumName = result.name,
                        ReleaseYear = result.release_date,
                        ArtistName = result.artists[0].name,
                        ImageUrl = result.images[0].url,
                        Label = result.label,
                        SpotifyAlbumId = spotifyAlbumId,
                        //todo : recupérer les tracks via methode
                        TrackList = _spotifyService.GetTracks(result),
                        Genres = await _spotifyService.GetGenres(result)
                    };

                    //on insère le vinyl dans la db
                    _vinylRepo.Insert(vinyl);

                    //on met à jour la wantlist du user
                    var currentUser = await _userManager.GetUserAsync(User);
                    var wantlist = new Wantlist()
                    {
                        UserId = currentUser.Id,
                        VinylId = vinyl.Id
                    };

                    //on insère dans la db
                    _listRepositoryAccessor("Wantlist").Insert(wantlist);

                    //succès et redirection vers la collection mise à jour
                    TempData["SuccessMessage"] = "Vinyl added successfully";
                    return RedirectToAction("DisplayMyWantlist");
                }

                //échec et redirection vers la collection non mise à jour
                TempData["ErrorMessage"] = "Vinyl not added, something went wrong";
                return RedirectToAction("DisplayMyWantlist");
            }
        }
        public IActionResult Delete(string vinylId)
        {
            if (_listRepositoryAccessor("Wantlist").Delete(vinylId) == true)
            {
                TempData["SuccessMessage"] = "Vinyl deleted";
                return RedirectToAction("DisplayMyCollection");
            }
            TempData["ErrorMessage"] = "Vinyl not deleted, something went wrong";
            return RedirectToAction("DisplayMyCollection");
        }
    }
}
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
using VinylStore.Common.Auth;
using VinylStore.Common.Contracts;
using VinylStore.Common.MTO;
using VinylStore.DAL.ExternalServices;
using VinylStore.DAL.ExternalServices.JsonModels;
using VinylStore.Models;
using VinylStore.ViewModels;

namespace VinylStore.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        //private string consumerKey = "QvviaqTYLJDSiYtyXjbE";
        //private string consumerSecret = "NajYtMXVBZnrYocbXRsCbWinUCPXgMXI";
        //private readonly ICollectionRepository _userVinylRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IVinylRepository _vinylRepo;        
        private readonly Func<string, IListRepository> _listRepositoryAccessor;
        private readonly ISpotifyService _spotifyService;

        public CollectionController(UserManager<ApplicationUser> userManager, IUserService userService, 
            IVinylRepository vinylRepo, Func<string, IListRepository> listRepositoryAccessor, ISpotifyService spotifyService)
        {
            _userManager = userManager;
            _userService = userService;            
            _vinylRepo = vinylRepo;            
            _listRepositoryAccessor = listRepositoryAccessor;
            _spotifyService = spotifyService;
        }

        public async Task<IActionResult> DisplayMyCollection()
        {           
            var currentUser = await _userManager.GetUserAsync(User);
            ViewBag.UserId = currentUser.Id;
            ViewBag.UserName = currentUser.UserName;

            var vinylCollection = _userService.GetMyCollection(currentUser.Id);             
            var vinylShortViewModelList = new List<VinylShortViewModel>();
            foreach (var vinylMTO in vinylCollection)
            {
                var shortModel = new VinylShortViewModel();

                shortModel.ImageUrl = vinylMTO.ImageUrl;
                shortModel.AlbumName = vinylMTO.AlbumName;
                shortModel.ArtistName = vinylMTO.ArtistName ?? "";
                shortModel.VinylId = vinylMTO.Id;
                
                vinylShortViewModelList.Add(shortModel);
            }
            return View(vinylShortViewModelList);
        }
        
        public async Task<IActionResult> AddToUserCollection(string spotifyAlbumId)
        {
            //2eme requete par id de l'album pour avoir les données complètes à sauver dans la table
            string queryString = "https://api.spotify.com/v1/albums/" + spotifyAlbumId;  
            
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _spotifyService.RefreshToken());
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(queryString);
                var output = await client.SendAsync(request);
                
                //on récupère le json de spotify
                var result = await output.Content.ReadAsAsync<AlbumIdSearchResultJsonModel>();

                //on vérifie si c'est pas vide
                if (result != null)
                {
                    VinylMTO vinyl = new VinylMTO()
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

                    //on insère le VinylMTO dans la db 
                    _vinylRepo.Insert(vinyl);

                    //on met à jour la collection du user
                    var currentUser = await _userManager.GetUserAsync(User);
                    var vinylForSale = new VinylForSaleMTO()
                    {
                        UserId = currentUser.Id,
                        VinylId = vinyl.Id
                    };

                    //on insère dans la db
                    _listRepositoryAccessor("VinylForSale").Insert(vinylForSale);

                    //succès et redirection vers la collection mise à jour
                    TempData["SuccessMessage"] = "Vinyl added successfully";
                    return RedirectToAction("DisplayMyCollection");
                }

                //échec et redirection vers la collection non mise à jour
                TempData["ErrorMessage"] = "Vinyl not added, something went wrong";
                return RedirectToAction("DisplayMyCollection");
            }                                       
        }

        public IActionResult Delete(string vinylId)
        {
            if(_listRepositoryAccessor("VinylForSale").Delete(vinylId) == true)
            {
                TempData["SuccessMessage"] = "Vinyl deleted";
                return RedirectToAction("DisplayMyCollection");
            }
            TempData["ErrorMessage"] = "Vinyl not deleted, something went wrong";
            return RedirectToAction("DisplayMyCollection");
        }
    }
}
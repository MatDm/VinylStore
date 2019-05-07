using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Abstract;
using VinylStore.Auth;
using VinylStore.Concrete;
using VinylStore.Models;
using VinylStore.Services;

namespace VinylStore.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        private string consumerKey = "QvviaqTYLJDSiYtyXjbE";
        private string consumerSecret = "NajYtMXVBZnrYocbXRsCbWinUCPXgMXI";
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IVinylRepository _vinylRepo;
        private readonly IUserVinylRepository _userVinylRepo;

        public CollectionController(UserManager<ApplicationUser> userManager, IUserService userService, IVinylRepository vinylRepo, IUserVinylRepository userVinylRepo)
        {
            _userManager = userManager;
            _userService = userService;            
            _vinylRepo = vinylRepo;
            _userVinylRepo = userVinylRepo;
        }

        public async Task<IActionResult> DisplayMyCollection()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            ViewBag.UserId = currentUser.Id;
            ViewBag.UserName = currentUser.UserName;

            var vinylCollection = _userService.GetMyCollection(currentUser.Id);
            //Todo : renvoyer un viewmodel et non une entité 
            return View(vinylCollection);
        }

        //albumName = item.title, genre = item.genre, imageUrl = item.cover_image, releaseYear = item.year 
        public async Task<IActionResult> AddToUserCollection(string albumName, string genre, string imageUrl, string releaseYear, string format)

        {

            // -------------------------------------- MARCHE A SUIVRE ------------------------------------- //

            // Proposition d'étape 0
            // Check si format est vinyl??

            // Proposition d'étape 1
            // Je crée un objet à partir des informations que je récupère
            var vinyl = new Vinyl() { AlbumName = albumName, Genre = genre, ImageUrl = imageUrl, ReleaseYear = releaseYear };
            // Proposition d'étape 2
            // Je l'ajoute à la base de donnée Vinyl
            _vinylRepo.Insert(vinyl);
            // Proposition d'étape 3
            // J'ajoute la référence(maintenant crée) entre l'utilisateur qui le rajoute à sa collection 
            // donc entre UserId et l'Id du Vinyl
            var currentUser = await _userManager.GetUserAsync(User);
            var userVinyl = new UserVinyl() { UserId = currentUser.Id, VinylId = vinyl.Id };
            _userVinylRepo.Insert(userVinyl);

            // Proposition d'étape 4
            //je redirige l'utilisateur vers sa collection (MISE a jour)

            //VICTOIRE


            return View();
            ////je vais chercher les info du vinyl avec son BARCODE
            //string urlApi = "https://api.discogs.com/database/search?id="
            //    + vinylId.ToString() + "&key=" + consumerKey + "&secret=" + consumerSecret;
            //using (var client = new HttpClient())
            //using (var request = new HttpRequestMessage())
            //{
            //    request.Headers.Add("User-Agent", "VinylStore");
            //    request.Method = HttpMethod.Get;
            //    request.RequestUri = new Uri(urlApi);
            //    var result = await client.SendAsync(request);
            //    //tester statuscode
            //    var parsedResult = await result.Content.ReadAsAsync<SearchVinyl>();
            //    return View(parsedResult);
            //}   



        }
    }
}
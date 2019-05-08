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
using VinylStore.ViewModels;

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
            var vinylShortViewModelList = new List<VinylShortViewModel>();
            foreach (var vinyl in vinylCollection)
            {
                var shortModel = new VinylShortViewModel();

                shortModel.ImageUrl = vinyl.ImageUrl;
                shortModel.AlbumName = vinyl.AlbumName;
                shortModel.ArtistName = vinyl.ArtistName ?? "";
                
                vinylShortViewModelList.Add(shortModel);
            }
            return View(vinylShortViewModelList);
        }
        
        public async Task<IActionResult> AddToUserCollection(string albumName, string genre, string imageUrl, string releaseYear, string format)
        {            
            // Check si format est vinyl??
            // Je crée un objet à partir des informations que je récupère
            var vinyl = new Vinyl() { AlbumName = albumName, Genre = genre, ImageUrl = imageUrl, ReleaseYear = releaseYear};
            // Je l'ajoute à la base de donnée Vinyl
            _vinylRepo.Insert(vinyl);
            // J'ajoute la référence(maintenant crée) entre l'utilisateur qui le rajoute à sa collection 
            // donc entre UserId et l'Id du Vinyl
            var currentUser = await _userManager.GetUserAsync(User);
            var userVinyl = new UserVinyl() { UserId = currentUser.Id, VinylId = vinyl.Id, IsPossessed = true };
            _userVinylRepo.Insert(userVinyl);
            //je redirige l'utilisateur vers sa collection (MISE a jour)
            return RedirectToAction("DisplayMyCollection");             
        }

        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> AddToUserWantlist(string albumName, string genre, string imageUrl, string releaseYear, string format)
        {
            var vinyl = new Vinyl() { AlbumName = albumName, Genre = genre, ImageUrl = imageUrl, ReleaseYear = releaseYear };
            _vinylRepo.Insert(vinyl);
            var currentUser = await _userManager.GetUserAsync(User);
            var userVinyl = new UserVinyl() { UserId = currentUser.Id, VinylId = vinyl.Id, IsPossessed = false };
            _userVinylRepo.Insert(userVinyl);
            return RedirectToAction("DisplayMyWantlist");            
        }
    }
}
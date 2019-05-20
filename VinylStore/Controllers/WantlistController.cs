using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Abstract;
using VinylStore.BLL.UseCases;
using VinylStore.Common.Auth;
using VinylStore.Common.Contracts;
using VinylStore.Common.MTO;
using VinylStore.DAL.ExternalServices;
using VinylStore.DAL.ExternalServices.JsonModels;
using VinylStore.Models;

using VinylStore.ViewModels;
using VinylStore.ViewModels.TypeExtentions;

namespace VinylStore.Controllers
{
    [Authorize]
    public class WantlistController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IVinylRepository _vinylRepo;
        private readonly Func<string, IListRepository> _listRepositoryAccessor;
        private readonly ISpotifyService _spotifyService;

        public WantlistController(UserManager<ApplicationUser> userManager, IVinylRepository vinylRepo,
            Func<string, IListRepository> listRepositoryAccessor, ISpotifyService spotifyService)
        {
            _userManager = userManager;
            _vinylRepo = vinylRepo;
            _listRepositoryAccessor = listRepositoryAccessor;
            _spotifyService = spotifyService;
        }

        public async Task<IActionResult> DisplayMyWantlist()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var UserRole = new UserUC(User.FindFirst(ClaimTypes.NameIdentifier).Value, _vinylRepo, _listRepositoryAccessor, _spotifyService);
            var vinylForSaleVM = UserRole.GetMyWantlist().Select(x => x.ToShortViewModel());

            ViewBag.UserId = currentUser.Id;
            ViewBag.UserName = currentUser.UserName;

            return View(vinylForSaleVM);
        }

        public IActionResult AddToUserWantlist(string spotifyAlbumId)
        {

            var UserRole = new UserUC(User.FindFirst(ClaimTypes.NameIdentifier).Value, _vinylRepo, _listRepositoryAccessor, _spotifyService);

            if (UserRole.AddToUserWantlist(spotifyAlbumId))
                TempData["SuccessMessage"] = "Vinyl added successfully";
            else
                TempData["ErrorMessage"] = "Vinyl not added, something went wrong";

            return RedirectToAction("DisplayMyWantlist");
        }

        public IActionResult Delete(string vinylId)
        {
            if (_listRepositoryAccessor("Wantlist").Delete(vinylId) == true)
            {
                TempData["SuccessMessage"] = "Vinyl deleted";
                return RedirectToAction("DisplayMyWantlist");
            }
            TempData["ErrorMessage"] = "Vinyl not deleted, something went wrong";
            return RedirectToAction("DisplayMyWantlist");
        }

        public IActionResult FindSellers()
        {
            var UserRole = new UserUC(User.FindFirst(ClaimTypes.NameIdentifier).Value, _vinylRepo, _listRepositoryAccessor, _spotifyService);
            var userList = UserRole.GetSellers().Select(x => x.ToShortViewModel());
        }
    }
    
}

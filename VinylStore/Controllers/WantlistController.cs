using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VinylStore.BLL.UseCases;
using VinylStore.Common.Auth;
using VinylStore.Common.Contracts;
using VinylStore.DAL.ExternalServices;
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
        private UserUC userRole
            => new UserUC(User.FindFirst(ClaimTypes.NameIdentifier).Value, _vinylRepo, _listRepositoryAccessor, _spotifyService);
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
           
            var vinylForSaleVM = userRole.GetMyWantlist().Select(x => x.ToShortViewModel());

            ViewBag.UserId = currentUser.Id;
            ViewBag.UserName = currentUser.UserName;

            return View(vinylForSaleVM);
        }

        public IActionResult AddToUserWantlist(string spotifyAlbumId)
        {           
            try
            {
                userRole.AddToUserWantlist(spotifyAlbumId);
                TempData["SuccessMessage"] = "Vinyl added successfully";                   
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Vinyl not added, something went wrong : {ex.Message}";
            }
            return RedirectToAction("DisplayMyWantlist");
        }

        public IActionResult Details(string vinylId)
        {
            var vinylViewModel = userRole.GetDetails(vinylId).ToViewModel();
            return View(vinylViewModel);
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
            var vinylForSaleList = userRole.GetSellers().Select(x => x.ToViewModel());

            return View(vinylForSaleList);
        }
    }
    
}

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
using VinylStore.Common.MTO;
using VinylStore.DAL.ExternalServices;
using VinylStore.ViewModels;
using VinylStore.ViewModels.TypeExtentions;

namespace VinylStore.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        //private string consumerKey = "QvviaqTYLJDSiYtyXjbE";
        //private string consumerSecret = "NajYtMXVBZnrYocbXRsCbWinUCPXgMXI";
        //private readonly ICollectionRepository _userVinylRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IVinylRepository _vinylRepo;
        private readonly Func<string, IListRepository> _listRepositoryAccessor;
        private readonly ISpotifyService _spotifyService;

        public CollectionController(UserManager<ApplicationUser> userManager,
            IVinylRepository vinylRepo, Func<string, IListRepository> listRepositoryAccessor, ISpotifyService spotifyService)
        {
            _userManager = userManager;
            _vinylRepo = vinylRepo;
            _listRepositoryAccessor = listRepositoryAccessor;
            _spotifyService = spotifyService;
        }

        public async Task<IActionResult> DisplayMyCollection()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var UserRole = new UserUC(User.FindFirst(ClaimTypes.NameIdentifier).Value, _vinylRepo,_listRepositoryAccessor, _spotifyService);
            var vinylForSaleVM = UserRole.GetMyCollectionForSales().Select(x => x.ToShortViewModel());

            ViewBag.UserId = currentUser.Id;
            ViewBag.UserName = currentUser.UserName;

            return View(vinylForSaleVM);
        }

        [HttpPost]
        public IActionResult AddToUserCollection(VinylMTO vinyl)
        {
            var UserRole = new UserUC(User.FindFirst(ClaimTypes.NameIdentifier).Value, _vinylRepo, _listRepositoryAccessor, _spotifyService);

            if (UserRole.AddToUserCollection(vinyl))
                TempData["SuccessMessage"] = "Vinyl added successfully";
            else
                TempData["ErrorMessage"] = "Vinyl not added, something went wrong";

            return RedirectToAction("DisplayMyCollection");

        }

        public IActionResult Delete(string vinylId)
        {
            if (_listRepositoryAccessor("VinylForSale").Delete(vinylId) == true)
            {
                TempData["SuccessMessage"] = "Vinyl deleted";
                return RedirectToAction("DisplayMyCollection");
            }
            TempData["ErrorMessage"] = "Vinyl not deleted, something went wrong";
            return RedirectToAction("DisplayMyCollection");
        }

        //affiche les details d'un vinyl
        public IActionResult Details(string vinylId)
        {
            var UserRole = new UserUC(User.FindFirst(ClaimTypes.NameIdentifier).Value, _vinylRepo, _listRepositoryAccessor, _spotifyService);
            var vinylForSaleViewModel = UserRole.GetDetails(vinylId).ToViewModel();            
            return View(vinylForSaleViewModel);
        }

        [HttpGet]
        public IActionResult Edit(string vinylId)
        {
            var UserRole = new UserUC(User.FindFirst(ClaimTypes.NameIdentifier).Value, _vinylRepo, _listRepositoryAccessor, _spotifyService);
            var vinyl = UserRole.GetDetails(vinylId).ToViewModel();
            return View(vinyl);
        }

        [HttpPost]
        public IActionResult Edit(VinylViewModel vinyl)
        {
            var UserRole = new UserUC(User.FindFirst(ClaimTypes.NameIdentifier).Value, _vinylRepo, _listRepositoryAccessor, _spotifyService);

            if (UserRole.EditVinyl(vinyl.ToMTO()))
                TempData["SuccessMessage"] = "Vinyl edited successfully";
            else
                TempData["ErrorMessage"] = "Vinyl not edited, something went wrong";

            //return vers la vue qui affiche les détails modifiés du vinyl (ou pas)
            return RedirectToAction("Details", new { vinylId = vinyl.Id});
        }

        [HttpGet]
        public IActionResult EditCreate(string spotifyAlbumId)
        {
            var vinyl = _spotifyService.GetVinylDetails(spotifyAlbumId);
            return View(vinyl);
        }             
    }
}

using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VinylStore.BLL.UseCases;
using VinylStore.Common.Auth;
using VinylStore.Common.Contracts;
using VinylStore.DAL.ExternalServices;
using VinylStore.Models;
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

        public IActionResult Buy(string vinylId, string buyerId)
        {
            try
            {
                string transactionId = Pay((double)userRole.GetDetails(vinylId).Price);
                HttpContext.Session.SetString("_TransactionId", transactionId);
                HttpContext.Session.SetString("_VinylId", vinylId);
                HttpContext.Session.SetString("_BuyerId", buyerId);
                return Redirect("https://localhost:44386/Home/Index?id=" + transactionId);
            }
            catch (Exception ex)
            {
                //write code to log error
                ErrorViewModel err = new ErrorViewModel
                {
                    RequestId = ex.Message
                };
                return View("~/Views/Shared/Error.cshtml", err);
            }
        }

        private string Pay(double total)
        {
            //if (total <= 0)
            //    return null;
            string urlEnd = "?clientToken=fvmlugXCy6D5AAQSvozahVnRIB7FOYRJsHO5ac1FKnOmZIcKNAysgpje8fMRsaVP&total=" + total.ToString();
            string totalUrl = "https://localhost:44386/api/Transaction" + urlEnd;
            string rep;

            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, totalUrl))
                {
                    var response = client.SendAsync(request);
                    Task<string> task = Task.Run<string>(async () => await response.Result.Content.ReadAsStringAsync());
                    rep = task.Result;
                }
            }
            return rep;
        }
    }
    
}

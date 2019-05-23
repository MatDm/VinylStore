using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VinylStore.Common.Auth;

namespace VinylStore.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ChatController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Chat()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            ViewBag.UserId = currentUser.Id;
            ViewBag.UserName = currentUser.UserName;

            return View();
        }
    }
}
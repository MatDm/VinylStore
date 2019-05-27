using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VinylStore.Common.Auth;
using VinylStore.ViewModels;

namespace VinylStore.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ChatController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Chat(string receiverName, string receiverId)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            ViewBag.UserId = currentUser.Id;
            ViewBag.UserName = currentUser.UserName;

            var receiverModel = new ReceiverViewModel()
            {
                ReceiverId = receiverId,
                ReceiverName = receiverName
            };

            return View("Chat",receiverModel);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Auth;
using VinylStore.Services;

namespace VinylStore.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public CollectionController(UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<IActionResult> MyCollection()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            ViewBag.UserId = currentUser.Id;
            ViewBag.UserName = currentUser.UserName;

            var vinylCollection = _userService.GetMyCollection(currentUser.Id);
            //Todo : renvoyer un viewmodel et non une entité 
            return View(vinylCollection);
        }
    }
}
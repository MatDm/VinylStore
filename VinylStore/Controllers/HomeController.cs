using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Abstract;
using VinylStore.BLL.UseCases;
using VinylStore.Common.Auth;
using VinylStore.Common.Contracts;
using VinylStore.DAL.ExternalServices;
using VinylStore.Models;
using VinylStore.ViewModels;
using VinylStore.ViewModels.TypeExtentions;

namespace VinylStore.Controllers
{

    public class HomeController : Controller
    {
        private GuestUC guestUC
            => new GuestUC(_vinylRepo);
        private readonly Func<string, IListRepository> _listRepositoryAccessor;
        private readonly ISpotifyService _spotifyService;
        private readonly IVinylRepository _vinylRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager,
            IVinylRepository vinylRepo, Func<string, IListRepository> listRepositoryAccessor, ISpotifyService spotifyService)
        {
            _vinylRepo = vinylRepo;
            _userManager = userManager;
            _listRepositoryAccessor = listRepositoryAccessor;
            _spotifyService = spotifyService;
        }
        public ViewResult Index()
        {
            //appeler methode du usecase qui prend un user au hasard et donne sa collection
            var vinyls = guestUC.GetRandomCollection().Select(x => x.ToViewModel()).ToArray();
            return View(vinyls);
            //return View(new List<VinylForSaleViewModel>());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

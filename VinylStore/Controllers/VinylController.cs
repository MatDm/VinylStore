using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Abstract;
using VinylStore.BLL.UseCases;
using VinylStore.Common.Auth;
using VinylStore.Common.Contracts;
using VinylStore.DAL.ExternalServices;
using VinylStore.ViewModels;
using VinylStore.ViewModels.TypeExtentions;

namespace VinylStore.Controllers
{
    public class VinylController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IVinylRepository _vinylRepository;
        private readonly Func<string, IListRepository> _listRepositoryAccessor;
        private readonly ISpotifyService _spotifyService;

        public VinylController(UserManager<ApplicationUser> userManager,
            IVinylRepository vinylRepository, Func<string, IListRepository> listRepositoryAccessor, ISpotifyService spotifyService)
        {
            _vinylRepository = vinylRepository;
        }
        public IActionResult List()
        {
            var vinyls = _vinylRepository.GetAllVinylMTOs().Select(v => v.ToShortViewModel()).ToList();
                      
            return View(vinyls);
        }

        //affiche les details d'un vinyl
        public IActionResult Details(string id)
        {
            var vinylViewModel = _vinylRepository.GetVinylMTOById(id).ToViewModel();
                  
            return View(vinylViewModel);
        }

        



    }
}
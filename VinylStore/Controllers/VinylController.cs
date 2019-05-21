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
            var vinyls = _vinylRepository.GetAllVinylMTOs();
            var vinylShortViewModels = new List<VinylShortViewModel>();
            foreach (var vinylMTO in vinyls)
            {
                var shortModel = new VinylShortViewModel()
                {
                    ImageUrl = vinylMTO.ImageUrl,
                    AlbumName = vinylMTO.AlbumName,
                    ArtistName = vinylMTO.ArtistName
                };
                vinylShortViewModels.Add(shortModel);
            }
            
            return View(vinylShortViewModels);
        }

        //affiche les details d'un vinyl
        public IActionResult Details(string id)
        {
            var vinylMTO = _vinylRepository.GetVinylMTOById(id);
            var vinylViewModel = new VinylViewModel()
            {
                Description = vinylMTO.Description,
                Id = vinylMTO.Id,
                ImageUrl = vinylMTO.ImageUrl,
                AlbumName = vinylMTO.AlbumName,
                ArtistName = vinylMTO.ArtistName,
                Genres = vinylMTO.Genres,
                ReleaseYear = vinylMTO.ReleaseYear,
                Price = vinylMTO.Price,
                TrackList = vinylMTO.TrackList                
            };                        
            return View(vinylViewModel);
        }

        



    }
}
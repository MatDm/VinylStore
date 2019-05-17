using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Abstract;
using VinylStore.Common.Contracts;
using VinylStore.ViewModels;

namespace VinylStore.Controllers
{
    public class VinylController : Controller
    {
        private readonly IVinylRepository _vinylRepository;

        public VinylController(IVinylRepository vinylRepository)
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
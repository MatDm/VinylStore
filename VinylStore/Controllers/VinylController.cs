using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Abstract;
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
            var vinyls = _vinylRepository.Get();
            var vinylShortViewModels = new List<VinylShortViewModel>();
            foreach (var vinyl in vinyls)
            {
                var shortModel = new VinylShortViewModel()
                {
                    ImageUrl = vinyl.ImageUrl,
                    AlbumName = vinyl.AlbumName,
                    ArtistName = vinyl.ArtistName
                };
                vinylShortViewModels.Add(shortModel);
            }
            
            return View(vinylShortViewModels);
        }

        public IActionResult Details(int id)
        {
            var vinyl = _vinylRepository.GetById(id);
            var vinylViewModel = new VinylViewModel()
            {
                ImageUrl = vinyl.ImageUrl,
                AlbumName = vinyl.AlbumName,
                ArtistName = vinyl.ArtistName,
                Genre = vinyl.Genre,
                ReleaseYear = vinyl.ReleaseYear,
                Price = vinyl.Price,
                TrackList = vinyl.TrackList
            };
            
            return View(vinylViewModel);
        }
    }
}
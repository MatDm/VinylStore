using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Abstract;
using VinylStore.Common.Contracts;
using VinylStore.Models;
using VinylStore.ViewModels;
using VinylStore.ViewModels.TypeExtentions;

namespace VinylStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVinylRepository _vinylRepository;
        public HomeController(IVinylRepository vinylRepository)
        {
            _vinylRepository = vinylRepository;
        }
        public ViewResult Index()
        {
            var vinyls = _vinylRepository.GetAllVinylMTOs().Select(v => v.ToShortViewModel()).ToList();

            return View(vinyls);
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

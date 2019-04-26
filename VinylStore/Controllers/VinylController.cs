using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Abstract;

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
            return View();
        }
    }
}
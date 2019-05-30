using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStore.Component
{
    public class UserMenu : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var menuItem = new UserMenuItem
            {
                DisplayValue = "Log off",
                ActionValue = "Logout"                
            };
            return View("UserMenu", menuItem);
        }
    }

    public class UserMenuItem
    {
        public string DisplayValue { get; set; }
        public string ActionValue { get; set; }
    }
}

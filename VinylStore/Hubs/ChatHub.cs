using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VinylStore.Common.Auth;

namespace VinylStore.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<ApplicationUser> userManager;


        public ChatHub(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task SendMessage(string user, string message)
        {
            //var User = await userManager.GetUserAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //await Clients.User(User).SendAsync("ReceiveMessage", user, message);
        }
    }
}

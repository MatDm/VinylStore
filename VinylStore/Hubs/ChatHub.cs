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
            //Groups.AddToGroupAsync(this.Context.ConnectionId, "mygroup", new System.Threading.CancellationToken());
        }
        public async Task SendMessage(string receiverId, string user, string message)
        {
            var currentTalker = await userManager.FindByNameAsync(user);

            //var User = await userManager.GetUserAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await Clients.User(receiverId).SendAsync("DisplayNewChatMessage", user, message);
            //await Clients.OthersInGroup("mygroup").SendAsync("ReceiveMessage", user, message);
        }
    }
}

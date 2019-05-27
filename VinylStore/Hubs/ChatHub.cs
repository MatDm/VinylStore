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
        public async Task SendMessage(string receiverName, string senderName, string message)
        {
            //var currentTalker = await userManager.FindByNameAsync(userName);
            var receiver = await userManager.FindByNameAsync(receiverName);
            var sender = await userManager.FindByNameAsync(senderName);
            //var User = await userManager.GetUserAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await Clients.Users(receiver.Id, sender.Id).SendAsync("DisplayNewChatMessage", senderName, message);
            //await Clients.OthersInGroup("mygroup").SendAsync("ReceiveMessage", user, message);
        }
    }
}

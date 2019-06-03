using System;
using System.Collections.Generic;
using System.Text;
using VinylStore.Common.Contracts;
using VinylStore.Common.Contracts.DAL;
using VinylStore.DAL.ExternalServices;

namespace VinylStore.BLL.UseCases
{
    public partial class UserUC : GuestUC
    {
        private readonly string userId;
        private readonly IVinylRepository vinylRepository;
        //private readonly ISpotifyProxy spotifyService;
        private readonly Func<string, IListRepository> listRepositoryAccessor;
        private readonly ISpotifyService spotifyService;
        //private readonly IUserRepository userRepository;

        public UserUC(string UserId, IVinylRepository vinylRepository, Func<string, IListRepository> listRepositoryAccessor, ISpotifyService spotifyService)
            : base(vinylRepository)
        {
            userId = UserId;
            this.vinylRepository = vinylRepository;
            this.listRepositoryAccessor = listRepositoryAccessor;
            this.spotifyService = spotifyService;
            //this.userRepository = userRepository;
        }

        
    }
}

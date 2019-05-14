using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VinylStore.Abstract;
using VinylStore.Models;

namespace VinylStore.Abstract
{
    public class UserService : IUserService
    {
        private readonly Func<string, IListRepository> _listRepositoryAccessor;
        private readonly IVinylRepository _vinylRepo;

        public UserService(Func<string, IListRepository> listRepositoryAccessor, IVinylRepository vinylRepo)
        {
            _listRepositoryAccessor = listRepositoryAccessor;
            _vinylRepo = vinylRepo;
        }
        public List<Vinyl> GetMyCollection(string userId)
        {
            var vinylForSaleTable = _listRepositoryAccessor("VinylForSale").GetAllVinylsForSale();          
            var collectionList = vinylForSaleTable.Where(u => u.UserId == userId).ToList();
            var vinylList = new List<Vinyl>();

            foreach (var collection in collectionList)
            {

                Vinyl vinyl = _vinylRepo.GetById(collection.VinylId.ToString());
                if (vinyl != null)
                {
                    vinylList.Add(vinyl);

                }
            }
            return vinylList;
        }

        public List<Vinyl> GetMyWantlist(string userId)
        {
            var wantlistTable = _listRepositoryAccessor("Wantlist").GetAllWantlists();
            var userVinylList = wantlistTable.Where(u => u.UserId == userId).ToList();
            var vinylList = new List<Vinyl>();

            foreach (var userVinyl in userVinylList)
            {
                Vinyl vinyl = _vinylRepo.GetById(userVinyl.VinylId.ToString());
                vinylList.Add(vinyl);
            }
            return vinylList;
        }

        public async Task<string> RefreshToken()
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                string urlToken = "https://accounts.spotify.com/api/token";
                request.Headers.Add("Authorization", "Basic MzM4YzVlZDI1NWEwNDA5NGEyNWIyZGFjMGZkNjYzMzU6ZGUzNDUwYWE3Y2JlNGVlNjgzOGQ1ZDhlODYyMDAyY2Q=");
                var body = new List<KeyValuePair<string, string>>();
                body.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                request.Content = new FormUrlEncodedContent(body);
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(urlToken);
                var result = await client.SendAsync(request);
                var accessTokenObject = await result.Content.ReadAsAsync<AccessTokenJsonModel>();
                return accessTokenObject.access_token;
            }
        }
    }
}

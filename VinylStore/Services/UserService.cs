using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VinylStore.Abstract;
using VinylStore.Concrete;
using VinylStore.Models;

namespace VinylStore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserVinylRepository _userVinylRepo;
        private readonly IVinylRepository _vinylRepo;

        public UserService(IUserVinylRepository userVinylRepo, IVinylRepository vinylRepo)
        {
            _userVinylRepo = userVinylRepo;
            _vinylRepo = vinylRepo;
        }
        public List<Vinyl> GetMyCollection(string userId)
        {

            var userVinylTable = _userVinylRepo.GetAll();
            var userVinylList = userVinylTable.Where(u => u.UserId == userId && u.IsPossessed == true).ToList();
            var vinylList = new List<Vinyl>();

            foreach (var userVinyl in userVinylList)
            {

                Vinyl vinyl = _vinylRepo.GetById(userVinyl.VinylId.ToString());
                if (vinyl != null)
                {
                    vinylList.Add(vinyl);

                }
            }
            return vinylList;
        }

        public List<Vinyl> GetMyWantlist(string userId)
        {
            var userVinylTable = _userVinylRepo.GetAll();
            var userVinylList = userVinylTable.Where(u => u.UserId == userId && u.IsPossessed == false).ToList();
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
                var accessTokenObject = await result.Content.ReadAsAsync<AccessToken>();
                return accessTokenObject.access_token;
            }
        }
    }
}

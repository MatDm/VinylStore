using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VinylStore.Models;

namespace VinylStore.Services
{
    public class SpotifyService
    {
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

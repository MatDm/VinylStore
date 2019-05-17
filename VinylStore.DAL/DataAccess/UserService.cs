//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Threading.Tasks;
//using VinylStore.Common.Contracts;
//using VinylStore.Common.MTO;

//namespace VinylStore.DAL.DataAccess
//{
//    public class UserService : IUserService
//    {
//        private readonly Func<string, IListRepository> _listRepositoryAccessor;
//        private readonly IVinylRepository _vinylRepo;

//        public UserService(Func<string, IListRepository> listRepositoryAccessor, IVinylRepository vinylRepo)
//        {
//            _listRepositoryAccessor = listRepositoryAccessor; 
//            _vinylRepo = vinylRepo;
//        }
//        //public List<VinylMTO> GetMyCollection(string userId) //mettre cette methode dans collectionRepository (dabord dans l'interface) -----> DONE
//        //{
//        //    //var vinylForSaleTable = _listRepositoryAccessor("VinylForSale").GetAllVinylsForSale();          
//        //    //var collectionList = vinylForSaleTable.Where(u => u.UserId == userId).ToList();
//        //    //var vinylList = new List<VinylMTO>();

//        //    //foreach (var collection in collectionList)
//        //    //{

//        //    //    VinylMTO vinyl = _vinylRepo.GetById(collection.VinylId.ToString());
//        //    //    if (vinyl != null)
//        //    //    {
//        //    //        vinylList.Add(vinyl);

//        //    //    }
//        //    //}
//        //    //return vinylList;
//        //}

//        //public List<VinylMTO> GetMyWantlist(string userId) // IDEM : a mettre dans wantlist repo
//        //{
//        //    var wantlistTable = _listRepositoryAccessor("Wantlist").GetAllWantlists();
//        //    var userVinylList = wantlistTable.Where(u => u.UserId == userId).ToList();
//        //    var vinylList = new List<VinylMTO>();

//        //    foreach (var userVinyl in userVinylList)
//        //    {
//        //        VinylMTO vinyl = _vinylRepo.GetById(userVinyl.VinylId.ToString());
//        //        vinylList.Add(vinyl);
//        //    }
//        //    return vinylList;
//        //}

//        //DOC
//        //public async Task<string> RefreshToken()
//        //{
//        //    using (var client = new HttpClient())
//        //    using (var request = new HttpRequestMessage())
//        //    {
//        //        string urlToken = "https://accounts.spotify.com/api/token";
//        //        request.Headers.Add("Authorization", "Basic MzM4YzVlZDI1NWEwNDA5NGEyNWIyZGFjMGZkNjYzMzU6ZGUzNDUwYWE3Y2JlNGVlNjgzOGQ1ZDhlODYyMDAyY2Q=");
//        //        var body = new List<KeyValuePair<string, string>>();
//        //        body.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
//        //        request.Content = new FormUrlEncodedContent(body);
//        //        request.Method = HttpMethod.Post;
//        //        request.RequestUri = new Uri(urlToken);
//        //        var result = await client.SendAsync(request);
//        //        var accessTokenObject = await result.Content.ReadAsAsync<AccessTokenJsonModel>();
//        //        return accessTokenObject.access_token;
//        //    }
//        //}
//    }
//}

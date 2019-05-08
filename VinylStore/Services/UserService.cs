using System;
using System.Collections.Generic;
using System.Linq;
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
                vinylList.Add(vinyl);
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Models;

namespace VinylStore.Concrete
{
    public class UserVinylRepository : IUserVinylRepository
    {
        private readonly VinylStoreDbContext _db;

        public UserVinylRepository(VinylStoreDbContext db)
        {
            _db = db;
        }

        public IEnumerable<UserVinyl> GetAll()
        {
            return _db.UserVinyls;
        }

        public void Insert(UserVinyl userVinyl)
        {
            _db.UserVinyls.Add(userVinyl);
            _db.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using VinylStore.Common.Auth;
using VinylStore.Common.Contracts.DAL;

namespace VinylStore.DAL.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private VinylStoreDbContext _db;
        public UserRepository(VinylStoreDbContext db)
        {
            _db = db;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _db.Users;
        }
    }
}

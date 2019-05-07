using System.Collections.Generic;
using VinylStore.Models;

namespace VinylStore.Concrete
{
    public interface IUserVinylRepository
    {
        IEnumerable<UserVinyl> GetAll();
        void Insert(UserVinyl userVinyl);
    }
}
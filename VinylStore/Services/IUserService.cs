using System.Collections.Generic;
using VinylStore.Models;

namespace VinylStore.Services
{
    public interface IUserService
    {
        List<Vinyl> GetMyCollection(string userId);
    }
}
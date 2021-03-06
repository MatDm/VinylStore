﻿using System.Collections.Generic;
using System.Threading.Tasks;
using VinylStore.Models;

namespace VinylStore.Abstract
{
    public interface IUserService
    {
        List<Vinyl> GetMyCollection(string userId);
        List<Vinyl> GetMyWantlist(string id);
        Task<string> RefreshToken();
    }
}
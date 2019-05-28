using System;
using System.Collections.Generic;
using System.Text;
using VinylStore.Common.Auth;

namespace VinylStore.Common.Contracts.DAL
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetAllUsers();
    }
}

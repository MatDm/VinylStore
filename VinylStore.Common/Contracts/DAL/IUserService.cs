using System.Collections.Generic;
using System.Threading.Tasks;
using VinylStore.Common.MTO;

namespace VinylStore.Common.Contracts
{
    public interface IUserService
    {
        List<VinylMTO> GetMyCollection(string userId);
        List<VinylMTO> GetMyWantlist(string id);
        //DOC Task<string> RefreshToken();
    }
}
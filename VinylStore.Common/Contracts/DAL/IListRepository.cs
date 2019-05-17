using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Common.MTO;

namespace VinylStore.Common.Contracts
{
    public interface IListRepository
    {
        IEnumerable<WantlistMTO> GetAllWantlistMTOs();
        IEnumerable<VinylForSaleMTO> GetAllVinylForSaleMTOs();
        //IEnumerable<VinylForSaleMTO> GetVinylForSaleMTOsByUserId(string userId);
        IEnumerable<WantlistMTO> GetWantlistMTOsByUserId(string userId);
        void Insert(WantlistMTO wantlist);
        void Insert(VinylForSaleMTO vinylForSale);

        bool Delete(string vinylId);
    }
}

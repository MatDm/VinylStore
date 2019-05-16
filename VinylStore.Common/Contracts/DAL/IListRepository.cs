using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Common.MTO;

namespace VinylStore.Common.Contracts
{
    public interface IListRepository
    {
        IEnumerable<WantlistMTO> GetAllWantlists();
        IEnumerable<VinylForSaleMTO> GetAllVinylsForSale();
        IEnumerable<VinylForSaleMTO> GetVinylsForSaleByUser(string userId);
        void Insert(WantlistMTO wantlist);
        void Insert(VinylForSaleMTO vinylForSale);

        bool Delete(string vinylId);
    }
}

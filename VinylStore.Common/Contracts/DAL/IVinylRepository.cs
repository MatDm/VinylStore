using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Common.Auth;
using VinylStore.Common.MTO;

namespace VinylStore.Common.Contracts
{
    public interface IVinylRepository
    {
        IEnumerable<VinylMTO> GetAllVinylMTOs();
        //Vinyl GetById(int id);
        VinylMTO GetVinylMTOById(string vinylId);
        string Insert(VinylMTO vinyl);
        bool Delete(string vinylId);
        IEnumerable<VinylMTO> GetMyWantlistByUserId(string userId);
        IEnumerable<VinylMTO> GetMyCollectionForSaleByUserId(string userId);
        IEnumerable<VinylForSaleMTO> GetAllVinylForSales();
        VinylMTO GetVinylForSaleDetail(string vinylId);       
        bool EditVinylForSaleDetail(VinylMTO vinyl);
        IEnumerable<VinylForSaleMTO> GetVinylSellers(string userId);
    }
}

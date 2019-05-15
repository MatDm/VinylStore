using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Models;

namespace VinylStore.Abstract
{
    public interface IListRepository
    {
        IEnumerable<Wantlist> GetAllWantlists();
        IEnumerable<VinylForSale> GetAllVinylsForSale();
        void Insert(Wantlist wantlist);
        void Insert(VinylForSale vinylForSale);

        bool Delete(string vinylId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Common.MTO;

namespace VinylStore.Common.Contracts
{
    public interface IVinylRepository
    {
        IEnumerable<VinylMTO> Get();
        //Vinyl GetById(int id);
        VinylMTO GetById(string id);

        void Insert(VinylMTO vinyl);

        bool Delete(string vinylId);



    }
}

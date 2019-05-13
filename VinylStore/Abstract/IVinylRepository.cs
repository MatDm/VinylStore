using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Models;

namespace VinylStore.Abstract
{
    public interface IVinylRepository
    {
        IEnumerable<Vinyl> Get();
        //Vinyl GetById(int id);
        Vinyl GetById(string id);

        void Insert(Vinyl vinyl);

        bool Delete(int vinylId);



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Models;

namespace VinylStore.Abstract
{
    interface IWantlistRepository
    {
        IEnumerable<Wantlist> GetAll();
        void Insert(Wantlist wantlist);
    }
}

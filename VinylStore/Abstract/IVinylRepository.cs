using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Entities;

namespace VinylStore.Abstract
{
    public interface IVinylRepository
    {
        IEnumerable<Vinyl> Get();
    }
}

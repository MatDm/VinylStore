using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Abstract;
using VinylStore.Models;

namespace VinylStore.Concrete
{
    public class VinylRepository : IVinylRepository
    {
        private VinylStoreDbContext _db;
        public VinylRepository(VinylStoreDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Vinyl> Get()
        {
            return _db.Vinyls;
        }

        public Vinyl GetById(int id)
        {
            var vinyl = _db.Vinyls.FirstOrDefault(v => v.Id == id);
            return vinyl;
        }
    }
}

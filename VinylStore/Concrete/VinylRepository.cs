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

        public bool Delete(int vinylId)
        {
            var vinyl = _db.Vinyls.FirstOrDefault(v => v.Id == vinylId);
            
            _db.Vinyls.Remove(vinyl);
            if (_db.SaveChanges()> 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Vinyl> Get()
        {
            return _db.Vinyls;
        }

        public Vinyl GetById(string id)
        {
            var vinyl = _db.Vinyls.FirstOrDefault(v => v.Id.ToString() == id);
            return vinyl;
        }

        public void Insert(Vinyl vinyl)
        {
            _db.Vinyls.Add(vinyl);
            _db.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Common.Contracts;
using VinylStore.Common.MTO;
using VinylStore.DAL.Extensions;

namespace VinylStore.DAL.DataAccess
{
    public class VinylRepository : IVinylRepository
    {
        private VinylStoreDbContext _db;
        public VinylRepository(VinylStoreDbContext db)
        {
            _db = db;
        }

        public bool Delete(string vinylId)
        {
            var vinylMTO = _db.Vinyls.FirstOrDefault(v => v.Id.ToString() == vinylId);
            
            _db.Vinyls.Remove(vinylMTO);
            if (_db.SaveChanges()> 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<VinylMTO> Get()
        {
            //todo transfomer VinylMTO en dto
            return _db.Vinyls.Select(x => x.ToDTO());
        }

        public VinylMTO GetById(string id)
        {
            var vinylMTO = _db.Vinyls.FirstOrDefault(v => v.Id.ToString() == id);
            return vinylMTO.ToDTO();
        }

        public void Insert(VinylMTO vinyl)
        {
            _db.Vinyls.Add(vinyl.ToEntity());
            _db.SaveChanges();
        }
    }
}

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
            var vinylMTO = _db.Vinyls.FirstOrDefault(v => v.Id == vinylId);
            
            _db.Vinyls.Remove(vinylMTO);
            if (_db.SaveChanges()> 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<VinylMTO> GetAllVinylMTOs()
        {
            
            return _db.Vinyls.Select(x => x.ToMTO());
        }

        public VinylMTO GetVinylMTOById(string vinylId)
        {
            var vinylEF = _db.Vinyls.FirstOrDefault(v => v.Id == vinylId);
            return vinylEF.ToMTO();
        }

        public string Insert(VinylMTO vinyl)
        {
            var vinylEF = vinyl.ToEntity();
            _db.Vinyls.Add(vinylEF);
            _db.SaveChanges();
            return vinylEF.Id;
        }

        public IEnumerable<VinylMTO> GetMyCollectionForSaleByUserId(string userId)
            => _db.Collections.Where(u => u.UserId == userId)
                                            .Select( x=> _db.Vinyls.FirstOrDefault(v => v.Id == x.VinylId).ToMTO());

        public IEnumerable<VinylMTO> GetMyWantlistByUserId(string userId)
        => _db.Wantlists.Where(u => u.UserId == userId)
                                            .Select(x => _db.Vinyls.FirstOrDefault(v => v.Id == x.VinylId).ToMTO());
    }
}

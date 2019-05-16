using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Common.Contracts;
using VinylStore.Common.MTO;
using VinylStore.DAL.Extensions;

namespace VinylStore.DAL.DataAccess
{
    public class WantlistRepository : IListRepository
    {
        private readonly VinylStoreDbContext _db;

        public WantlistRepository(VinylStoreDbContext db)
        {
            _db = db;
        }

        public IEnumerable<WantlistMTO> GetAllWantlists()
        {
            return _db.Wantlists.Select(x => x.ToDTO());
        }               

        public void Insert(WantlistMTO wantlist)
        {
            _db.Wantlists.Add(wantlist.ToEntity());
            _db.SaveChanges();
        }

        public bool Delete(string vinylId)
        {
            var vinylMTO = _db.Wantlists.FirstOrDefault(w => w.VinylId == vinylId);

            _db.Wantlists.Remove(vinylMTO);
            if (_db.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }


        //---------------------NOT USED---------------------------

        public void Insert(VinylForSaleMTO wantlist)
        {
            throw new NotImplementedException();
        }

        
        public IEnumerable<VinylForSaleMTO> GetAllVinylsForSale()
        {
            throw new NotImplementedException();
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Models;

namespace VinylStore.Abstract
{
    public class VinylForSaleRepository : IListRepository
    {
        private readonly VinylStoreDbContext _db;

        public VinylForSaleRepository(VinylStoreDbContext db)
        {
            _db = db;
        }

        public IEnumerable<VinylForSale> GetAllVinylsForSale()
        {
            return _db.Collections;
        }

        public void Insert(VinylForSale collection)
        {
            _db.Collections.Add(collection);
            _db.SaveChanges();
        }




        //---------------------------------------------------

        public void Insert(Wantlist wantlist)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Wantlist> GetAllWantlists()
        {
            throw new NotImplementedException();
        }
    }
}

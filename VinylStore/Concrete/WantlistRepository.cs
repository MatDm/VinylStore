﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Models;

namespace VinylStore.Abstract
{
    public class WantlistRepository : IListRepository
    {
        private readonly VinylStoreDbContext _db;

        public WantlistRepository(VinylStoreDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Wantlist> GetAllWantlists()
        {
            return _db.Wantlists;
        }               

        public void Insert(Wantlist wantlist)
        {
            _db.Wantlists.Add(wantlist);
            _db.SaveChanges();
        }


        //------------------------------------------------

        public void Insert(VinylForSale wantlist)
        {
            throw new NotImplementedException();
        }

        
        public IEnumerable<VinylForSale> GetAllVinylsForSale()
        {
            throw new NotImplementedException();
        }
    }
}

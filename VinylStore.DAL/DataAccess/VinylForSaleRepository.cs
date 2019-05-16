﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.DAL.Extensions;
using VinylStore.Common.Contracts;
using VinylStore.Common.MTO;
using VinylStore.DAL.Entities;

namespace VinylStore.DAL.DataAccess
{
    public class VinylForSaleRepository : IListRepository
    {
        private readonly VinylStoreDbContext _db;

        public VinylForSaleRepository(VinylStoreDbContext db)
        {
            _db = db;
        }

        public IEnumerable<VinylForSaleMTO> GetAllVinylsForSale()
        {
            return _db.Collections.Select(x=>x.ToDTO());
        }

        public IEnumerable<VinylForSaleMTO> GetVinylsForSaleByUser(string userId)
        {
            // sélectionne les vinyls à vendre en fonction de l'Id d'un utilisateur
            var vinylsForSaleEF = _db.Collections.Where(u => u.Id == userId);

            // initialise une nouvelle liste qui sera la valeur retournée
            List<VinylForSaleMTO> vinylsForSaleMTO = new List<VinylForSaleMTO>();

            // boucle dans la liste des vinyls à vendre
            // et les convertit en une liste de MTO
            foreach (var vinylForSaleEF in vinylsForSaleEF)
            {
                vinylsForSaleMTO.Add(vinylForSaleEF.ToMTO());
            }

            return vinylsForSaleMTO;

            //var vinylsMTO = new List<VinylMTO>();

            //foreach (var vinylForSale in vinylsForSale)
            //{                
            //    VinylMTO vinylMTO = _db.Vinyls.FirstOrDefault(v => v.Id == vinylForSale.VinylId).ToMTO();                
            //    if (vinylMTO != null)
            //    {
            //        vinylsMTO.Add(vinylMTO);
            //    }
            //}
            //return vinylsMTO;
        }

        public void Insert(VinylForSaleMTO vinylForSaleDTO)
        {
            _db.Collections.Add(vinylForSaleDTO.ToEntity());
            _db.SaveChanges();
        }

        public bool Delete(string vinylId)
        {
            var vinylMTO = _db.Collections.FirstOrDefault(c => c.VinylId == vinylId);

            _db.Collections.Remove(vinylMTO);
            if (_db.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }


        //----------------------NOT USED-----------------------------

        public void Insert(WantlistMTO wantlist)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WantlistMTO> GetAllWantlists()
        {
            throw new NotImplementedException();
        }

        
    }
}

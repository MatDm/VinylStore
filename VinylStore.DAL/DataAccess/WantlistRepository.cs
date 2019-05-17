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

        public IEnumerable<WantlistMTO> GetAllWantlistMTOs()
        {
            return _db.Wantlists.Select(x => x.ToMTO());
        }   
        
        public IEnumerable<WantlistMTO> GetWantlistMTOsByUserId(string userId)
        {
            //sélection de la wantlist par l'id du user
            var wantlistsEF = _db.Wantlists.Where(w => w.UserId == userId);

            //initialisation de la liste de wantlistMTO qui sera retournée
            List<WantlistMTO> wantlistMTOs = new List<WantlistMTO>();

            //boucle dans la liste d'entité pour en faire des MTO et les mettre dans la liste de mto
            foreach (var wantlistEF in wantlistsEF)
            {
                wantlistMTOs.Add(wantlistEF.ToMTO());
            }
            return wantlistMTOs;
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

        public void Insert(VinylForSaleMTO vinylForSaleMTO)
        {
            throw new NotImplementedException();
        }

        
        public IEnumerable<VinylForSaleMTO> GetAllVinylForSaleMTOs()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VinylForSaleMTO> GetVinylForSaleMTOsByUserId(string userId)
        {
            throw new NotImplementedException();
        }     
    }
}

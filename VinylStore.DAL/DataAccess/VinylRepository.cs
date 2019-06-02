using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Common.Auth;
using VinylStore.Common.Contracts;
using VinylStore.Common.MTO;
using VinylStore.DAL.Entities;
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
            if (_db.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<VinylMTO> GetAllVinylMTOs()
        {

            return _db.Vinyls.Select(x => x.ToMTO());
        }

        public IEnumerable<VinylForSaleMTO> GetAllVinylForSales()
        {

            return _db.Collections.Include(i => i.Vinyl).Include(i => i.User).Select(x => x.ToMTO());
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
                                            .Select(x => _db.Vinyls.FirstOrDefault(v => v.Id == x.VinylId).ToMTO());

        public IEnumerable<VinylMTO> GetMyWantlistByUserId(string userId)
        => _db.Wantlists.Where(u => u.UserId == userId)
                                            .Select(x => _db.Vinyls.FirstOrDefault(v => v.Id == x.VinylId).ToMTO());

        public VinylMTO GetVinylForSaleDetail(string vinylId)
        => _db.Vinyls.FirstOrDefault(v => v.Id == vinylId).ToMTO();

        public bool EditVinylForSaleDetail(VinylMTO vinyl)
        {           
            var vinylEF = vinyl.ToEntity();            
            _db.Set<VinylEF>().Attach(vinylEF).State = EntityState.Modified;
            _db.SaveChanges();    
            
            return true;
        }

        public IEnumerable<VinylForSaleMTO> GetVinylSellers(string userId)
        {
            var vinylWanted = GetMyWantlistByUserId(userId);

            var ReturnValue = new List<VinylForSaleMTO>();

            foreach (var item in vinylWanted)
            {
                ReturnValue
                    .AddRange(
                        _db.Collections.Where(c => c.Vinyl.SpotifyAlbumId == item.SpotifyAlbumId)
                            .Include(i=> i.Vinyl)
                            .Include(i=> i.User)
                            .Select( vFs => vFs.ToMTO())
                            .ToList()
                    );
            }
            return ReturnValue;
        }

        //public IEnumerable<VinylForSaleMTO> GetRandomCollectionForSale(int randomNumber)
        //{
        //    var vinyls = GetAllVinylForSales();
        //    return vinyls.Skip(randomNumber).Take(3);
        //}
    }
}

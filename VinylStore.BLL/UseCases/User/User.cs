using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylStore.Common.Auth;
using VinylStore.Common.MTO;

namespace VinylStore.BLL.UseCases
{
    public partial class UserUC : GuestUC
    {
        public IEnumerable<VinylMTO> GetMyCollectionForSales()
            => vinylRepository.GetMyCollectionForSaleByUserId(userId);

        public IEnumerable<VinylMTO> GetMyWantlist()
            => vinylRepository.GetMyWantlistByUserId(userId);

        public bool AddToUserCollection(VinylMTO vinyl)
        {
            try
            {
                //var vinylMTO = spotifyRepository.GetVinylDetails(SpotifyAlbumID);

                if (vinyl != null)
                {
                    //on insère le VinylMTO dans la db 
                    var vinylEFid = vinylRepository.Insert(vinyl);

                    //on met à jour la collection du user
                    var vinylForSale = new VinylForSaleMTO()
                    {
                        UserId = userId,
                        VinylId = vinylEFid
                    };

                    //on insère dans la db
                    listRepositoryAccessor("VinylForSale").Insert(vinylForSale);

                    return true;
                }
                else
                    return false;
            }

            catch (Exception)
            {
                return false;
            }

        }

        public bool AddToUserWantlist(string spotifyAlbumId)
        {
            try
            {
                var vinylMTO = spotifyService.GetVinylDetails(spotifyAlbumId);

                if (vinylMTO != null)
                {
                    //on insère le VinylMTO dans la db 
                    var vinylEFid = vinylRepository.Insert(vinylMTO);

                    //on met à jour la collection du user
                    var wantlistMTO = new WantlistMTO()
                    {
                        UserId = userId,
                        VinylId = vinylEFid
                    };

                    //on insère dans la db
                    listRepositoryAccessor("Wantlist").Insert(wantlistMTO);

                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("AddToUserWantlist: une erreur est intervenue dans les processus d'ajout à la wantlist", ex);
            }

        }

        public VinylMTO GetDetails(string vinylId)
            => vinylRepository.GetVinylForSaleDetail(vinylId);

        public bool EditVinyl(VinylMTO vinyl)
            => vinylRepository.EditVinylForSaleDetail(vinyl);

        public IEnumerable<VinylForSaleMTO> GetSellers()
        {
            return vinylRepository.GetVinylSellers(userId);
        }

        public List<VinylMTO> SearchByAlbum(string query, string artistName = "",
            string year = "",
            string genre = "",
            string upc = "",
            string isrc = "",
            int limit = 20,
            int offset = 0)
        {
            var album = spotifyService.GetVinyls(query, artistName, year, genre, upc, isrc, limit, offset);
            return album;
        }


    }
}

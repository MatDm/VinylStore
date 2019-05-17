using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylStore.Common.MTO;

namespace VinylStore.BLL.UseCases
{
    public partial class UserUC : GuestUC
    {
        public IEnumerable<VinylMTO> GetMyCollectionForSales()
            => vinylRepository.GetMyCollectionForSaleByUserId(userId);

        public IEnumerable<VinylMTO> GetMyWantlist()
            => vinylRepository.GetMyWantlistByUserId(userId);

        public bool AddToUserCollection(string SpotifyAlbumID)
        {
            try
            {
                var vinylMTO = spotifyRepository.GetVinylDetails(SpotifyAlbumID);

                if (vinylMTO != null)
                {
                    //on insère le VinylMTO dans la db 
                    vinylRepository.Insert(vinylMTO);

                    //on met à jour la collection du user
                    var vinylForSale = new VinylForSaleMTO()
                    {
                        UserId = userId,
                        VinylId = vinylMTO.Id
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
                var vinylMTO = spotifyRepository.GetVinylDetails(spotifyAlbumId);

                if (vinylMTO != null)
                {
                    //on insère le VinylMTO dans la db 
                    vinylRepository.Insert(vinylMTO);

                    //on met à jour la collection du user
                    var wantlistMTO = new WantlistMTO()
                    {
                        UserId = userId,
                        VinylId = vinylMTO.Id
                    };

                    //on insère dans la db
                    listRepositoryAccessor("Wantlist").Insert(wantlistMTO);

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
    }
}

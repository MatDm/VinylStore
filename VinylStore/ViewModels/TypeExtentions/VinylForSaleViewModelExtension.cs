using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Common.MTO;

namespace VinylStore.ViewModels.TypeExtentions
{
    public static class VinylForSaleViewModelExtension
    {

        public static VinylForSaleViewModel ToViewModel(this VinylForSaleMTO vinylForSaleMTO)
        {
            var vinylViewModel = new VinylForSaleViewModel()
            {
                Id = vinylForSaleMTO.Id,
                UserId = vinylForSaleMTO.UserId,
                User = vinylForSaleMTO.User,
                VinylId = vinylForSaleMTO.VinylId,
                Vinyl = vinylForSaleMTO.Vinyl.ToViewModel()
            };
            return vinylViewModel;
        }

        //public static VinylMTO ToMTO(this VinylViewModel vinyl)
        //{
        //    var vinylMTO = new VinylMTO()
        //    {
        //        AlbumName = vinyl.AlbumName,
        //        ArtistName = vinyl.ArtistName,
        //        ImageUrl = vinyl.ImageUrl,
        //        Id = vinyl.Id,
        //        Description = vinyl.Description,
        //        Genres = vinyl.Genres,
        //        Price = vinyl.Price,
        //        ReleaseYear = vinyl.ReleaseYear,
        //        TrackList = vinyl.TrackList,
        //        Label = vinyl.Label
        //    };
        //    return vinylMTO;
        //}

    }
}

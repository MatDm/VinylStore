using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Common.MTO;

namespace VinylStore.ViewModels.TypeExtentions
{
    public static class VinylShortViewModelExtention
    {
        public static VinylShortViewModel ToShortViewModel(this VinylMTO vinylMTO)
        {
            var vinylShortViewModel = new VinylShortViewModel()
            {
                AlbumName = vinylMTO.AlbumName,
                ArtistName = vinylMTO.ArtistName,
                ImageUrl = vinylMTO.ImageUrl,
                VinylId = vinylMTO.Id
            };
            return vinylShortViewModel;
        }


    }
}

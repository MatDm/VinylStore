using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Common.MTO;

namespace VinylStore.ViewModels.TypeExtentions
{
    public static class VinylViewModelExtension
    {
        public static VinylViewModel ToViewModel(this VinylMTO vinylMTO)
        {
            var vinylViewModel = new VinylViewModel()
            {
                AlbumName = vinylMTO.AlbumName,
                ArtistName = vinylMTO.ArtistName,
                ImageUrl = vinylMTO.ImageUrl,
                Id = vinylMTO.Id,
                Description = vinylMTO.Description,
                Genres = vinylMTO.Genres,
                Price = vinylMTO.Price,
                ReleaseYear = vinylMTO.ReleaseYear,
                TrackList = vinylMTO.TrackList,
                Label = vinylMTO.Label

            };
            return vinylViewModel;
        }

        public static VinylMTO ToMTO(this VinylViewModel vinyl)
        {
            var vinylMTO = new VinylMTO()
            {
                AlbumName = vinyl.AlbumName,
                ArtistName = vinyl.ArtistName,
                ImageUrl = vinyl.ImageUrl,
                Id = vinyl.Id,
                Description = vinyl.Description,
                Genres = vinyl.Genres,
                Price = vinyl.Price,
                ReleaseYear = vinyl.ReleaseYear,
                TrackList = vinyl.TrackList,
                Label = vinyl.Label
            };
            return vinylMTO;
        }
    }
}

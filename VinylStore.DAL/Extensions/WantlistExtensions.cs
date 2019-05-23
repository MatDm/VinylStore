using System;
using System.Collections.Generic;
using System.Text;
using VinylStore.Common.MTO;
using VinylStore.DAL.Entities;

namespace VinylStore.DAL.Extensions
{
    public static class WantlistExtensions
    {
        public static WantlistMTO ToMTO(this WantlistEF wantlistEntity)
            => new WantlistMTO
            {
                Id = wantlistEntity.Id,
                UserId = wantlistEntity.UserId,
                VinylId = wantlistEntity.VinylId,
            };

        public static WantlistEF ToEntity(this WantlistMTO wantlistDTO)
            => new WantlistEF
            {
                
                UserId = wantlistDTO.UserId,
                VinylId = wantlistDTO.VinylId,
            };
    }
}

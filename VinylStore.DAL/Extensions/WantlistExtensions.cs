﻿using System;
using System.Collections.Generic;
using System.Text;
using VinylStore.Common.MTO;
using VinylStore.DAL.Entities;

namespace VinylStore.DAL.Extensions
{
    public static class WantlistExtensions
    {
        public static WantlistMTO ToDTO(this WantlistEF wantlistEntity)
            => new WantlistMTO
            {
                Id = wantlistEntity.Id,
                UserId = wantlistEntity.UserId,
                VinylId = wantlistEntity.VinylId,
            };

        public static WantlistEF ToEntity(this WantlistMTO wantlistDTO)
            => new WantlistEF
            {
                Id = wantlistDTO.Id,
                UserId = wantlistDTO.UserId,
                VinylId = wantlistDTO.VinylId,
            };
    }
}

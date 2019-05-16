﻿using System;
using System.Collections.Generic;
using System.Text;
using VinylStore.Common.MTO;
using VinylStore.DAL.Entities;

namespace VinylStore.DAL.Extensions
{
    public static class VinylForSaleExtensions
    {
        public static VinylForSaleMTO ToMTO(this VinylForSaleEF vinylForSaleEntity)
            => new VinylForSaleMTO
            {
                Id = vinylForSaleEntity.Id,
                UserId = vinylForSaleEntity.UserId,
                VinylId = vinylForSaleEntity.VinylId,
            };

        public static VinylForSaleEF ToEntity(this VinylForSaleMTO dto)
            => new VinylForSaleEF
            {
                Id = dto.Id,
                UserId = dto.UserId,
                VinylId = dto.VinylId,
            };
    }
}

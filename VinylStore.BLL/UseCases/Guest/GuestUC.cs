using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VinylStore.Common.Contracts;
using VinylStore.Common.MTO;

namespace VinylStore.BLL.UseCases
{
    public partial class GuestUC
    {
        private readonly IVinylRepository vinylRepository;

        public GuestUC(IVinylRepository vinylRepository)
        {
            if (vinylRepository == null)
            {
                throw new ArgumentNullException(nameof(vinylRepository));
            }

            this.vinylRepository = vinylRepository;
        }

        public IEnumerable<VinylForSaleMTO> GetRandomCollection()
        {
            Random random = new Random();
            int randomNumber = random.Next(5);

            var vinyls = vinylRepository.GetAllVinylForSales();
            return vinyls.Skip(randomNumber).Take(3);
        }
    }


}

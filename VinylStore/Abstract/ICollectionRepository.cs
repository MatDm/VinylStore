using System.Collections.Generic;
using VinylStore.Models;

namespace VinylStore.Abstract
{
    public interface ICollectionRepository
    {
        IEnumerable<Collection> GetAll();
        void Insert(Collection collection);
    }
}
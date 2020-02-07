using System;
using System.Collections.Generic;

namespace PaymentApp.DAL.Repositories
{
    public interface IRepository<T>
    {
        void Add(T obj);
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        void Commit();
    }
}
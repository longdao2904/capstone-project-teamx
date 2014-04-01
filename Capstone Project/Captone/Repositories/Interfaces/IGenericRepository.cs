using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Captone.Models;

namespace Captone.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetByQuery(Func<T, bool> predicate);
        T GetById(int id);
        void Insert(T entity);
        void Delete(int id);
        void Update(T trip);
        void UpdateList(List<T> trip);
    }
}


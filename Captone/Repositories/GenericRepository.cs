using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using Captone.Models;
using Captone.Repositories.Interfaces;

namespace Captone.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected IUnitOfWork UnitOfWork { get; set; }

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public GenericRepository()
        {

        }
        private iDeliverEntities Context
        {
            get { return (iDeliverEntities)UnitOfWork; }
        }

        public virtual IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }

        private DbSet<T> DbSet()
        {
            return Context.Set<T>();
        }

        public T GetById(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public IQueryable<T> GetByQuery(Func<T, bool> predicate)
        {
            return Context.Set<T>().Where(predicate).AsQueryable();
        }

        public void Insert(T entity)
        {
            DbSet().Add(entity);
            UnitOfWork.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = Context.Set<T>().Find(id);
            Context.Set<T>().Remove(entity);
            UnitOfWork.SaveChanges();
        }

        public void Update(T entity)
        {
            DbSet().Attach(entity);
            SetEntityState(entity, EntityState.Modified);
            UnitOfWork.SaveChanges();
        }

        public void UpdateList(List<T> entities)
        {
            foreach (var entity in entities)
            {
                DbSet().Attach(entity);
                SetEntityState(entity, EntityState.Modified);
            }
            UnitOfWork.SaveChanges();
        }

        private void SetEntityState(object entity, EntityState entityState)
        {
            Context.Entry(entity).State = entityState;
        }
    }
}
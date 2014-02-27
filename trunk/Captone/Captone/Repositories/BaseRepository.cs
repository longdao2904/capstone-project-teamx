using System;
using System.Data;
using System.Data.Entity;
using Captone.Models;
using Captone.Repositories.Interfaces;

namespace Captone.Repositories
{
    public class BaseRepository
    {
        protected IUnitOfWork UnitOfWork { get; set; }

        protected iDeliverEntities Context
        {
            get { return (iDeliverEntities)UnitOfWork; }
        }

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            UnitOfWork = unitOfWork;
        }

        protected virtual DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>();
        }

        protected virtual void SetEntityState(object entity, EntityState entityState)
        {
            Context.Entry(entity).State = entityState;
        }

        protected virtual void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            GetDbSet<TEntity>().Add(entity);
            UnitOfWork.SaveChanges();
        }

        protected virtual void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            GetDbSet<TEntity>().Remove(entity);
            UnitOfWork.SaveChanges();
        }

        protected virtual void Update<TEntity>(TEntity entity) where TEntity : class
        {
            GetDbSet<TEntity>().Attach(entity);
            SetEntityState(entity, EntityState.Modified);
            UnitOfWork.SaveChanges();
        }

        protected virtual DbSet<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>();
        }
    }
}
﻿using General.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace General.Entities 
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private GeneralDbContext _dbContext;

        public EfRepository(GeneralDbContext generalDbContext)
        {
            this._dbContext = generalDbContext;
        }

        public DbContext DbContext
        {
            get { return _dbContext; }
        }

        public DbSet<TEntity> Entities
        {
            get { return _dbContext.Set<TEntity>(); }
        }

        public IQueryable<TEntity> Table
        {
            get { return Entities; }
        }

        public void Delete(TEntity entity, bool isSave = true)
        {
            Entities.Remove(entity);
            if (isSave)
            {
                _dbContext.SaveChanges();
            }
        }

        public TEntity GetById(object id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public void Insert(TEntity entity, bool isSave = true)
        {
            Entities.Add(entity);
            if (isSave)
            {
                _dbContext.SaveChanges();
            }
        }

        public void Update(TEntity entity, bool isSave = true)
        {
            if (isSave)
            {
                _dbContext.SaveChanges();
            }
        }
    }
}

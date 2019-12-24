using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace General.Core.Data
{
    /// <summary>
    /// 仓储模型
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        DbContext DbContext { get; }
        DbSet<TEntity> Entities { get; }
        IQueryable<TEntity> Table { get; }
        /// <summary>
        /// 通过主键Id获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(object id);
        void Insert(TEntity entity, bool isSave = true);
        void Update(TEntity entity, bool isSave = true);
        void Delete(TEntity entity, bool isSave = true);

    }
}

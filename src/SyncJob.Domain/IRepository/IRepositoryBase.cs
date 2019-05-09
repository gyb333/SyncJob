
using Entitys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace IRepository
{
    public interface IRepositoryBase<TEntity> :IRepository<TEntity>
        where TEntity : class, IEntity
    {
        Task BatchInsertAsync(IList<TEntity> entites);

        Task BatchUpdateAsync(IList<TEntity> entites);

        Task BatchDeleteAsync(IList<TEntity> entites);


        Task BatchMergeAsync(IList<TEntity> entites);
    }


    public interface IRepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
       where TEntity : class, IEntity<TKey>
    {
        Task BatchInsertAsync(IList<TEntity> entites);

        Task BatchUpdateAsync(IList<TEntity> entites);

        Task BatchDeleteAsync(IList<TEntity> entites);


        Task BatchMergeAsync(IList<TEntity> entites);
    }
}

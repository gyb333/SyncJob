
using Entitys;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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

 
        IEnumerable<TEntity> GetItemsByKeys([NotNull]IEnumerable<IEntity<TKey>> entities, string sql,int PageSize= 2000);

        IEnumerable<TEntity> GetItemsByTempTable([NotNull]IEnumerable<IEntity<TKey>> entities, string sql, string strTableName="Ids");
    }
}

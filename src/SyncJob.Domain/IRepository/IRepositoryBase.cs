
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


        

        Task<int> ExecuteSqlCommandAsync([NotNull] string sql, [NotNull] params object[] parameters);

        Task<int> ExecuteSqlCommandAsync([NotNull]  string sql, [NotNull] IEnumerable<object> parameters, CancellationToken cancellationToken = default);
        Task<int> ExecuteSqlCommandAsync([NotNull] string  sql, CancellationToken cancellationToken = default);

        Task<int> ExecuteSqlCommandAsync([NotNull] FormattableString sql, CancellationToken cancellationToken = default);
    }


    public interface IRepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
       where TEntity : class, IEntity<TKey>
    {
        Task BatchInsertAsync(IList<TEntity> entites);

        Task BatchUpdateAsync(IList<TEntity> entites);

        Task BatchDeleteAsync(IList<TEntity> entites);


        Task BatchMergeAsync(IList<TEntity> entites);


        Task<int> ExecuteSqlCommandAsync([NotNull] string sql, [NotNull] params object[] parameters);

        Task<int> ExecuteSqlCommandAsync([NotNull]  string sql, [NotNull] IEnumerable<object> parameters, CancellationToken cancellationToken = default);
        Task<int> ExecuteSqlCommandAsync([NotNull] string sql, CancellationToken cancellationToken = default);

        Task<int> ExecuteSqlCommandAsync([NotNull] FormattableString sql, CancellationToken cancellationToken = default);


        IEnumerable<TEntity> GetKeys([NotNull]IEnumerable<TEntity> entities, string sql,int PageSize= 100);
    }
}

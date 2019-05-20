
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Study.Common.Domain
{
    public class ManagerBase<TEntity> : DomainService
          where TEntity : class, IEntity
    {
        private readonly IRepositoryBase<TEntity> _repositoryBase;
        public ManagerBase(IRepositoryBase<TEntity> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

       

        public async Task BatchInsertAsync(IList<TEntity> entites)
        {
            //_repositoryBase
        }

        public async Task BatchUpdateAsync(IList<TEntity> entites)
        {

        }

        public async Task BatchDeleteAsync(IList<TEntity> entites)
        {

        }


        public async Task BatchMergeAsync(IList<TEntity> entites)
        {

        }

    }
}

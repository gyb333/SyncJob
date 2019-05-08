
using Entitys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace IRepository
{
    public interface IUserTargetRepository:IRepository<UserTarget>
    {
        Task BatchInsert(IList<UserTarget> entites);
    }
}

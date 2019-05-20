
using Domain;
 
using Entitys;
using Microsoft.EntityFrameworkCore;
using Study.Common.EFCore;
using Volo.Abp.Data;
 

namespace SyncJob.EntityFrameworkCore
{
    [ConnectionStringName(SourceDbConsts.ConnectionStringName)]
    public interface ISourceDbContext : IDbContextBase
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */

        DbSet<User> Users { get; }
    }
}
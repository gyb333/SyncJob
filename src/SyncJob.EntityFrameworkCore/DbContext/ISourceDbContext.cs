
using Domain;
using EntityFrameworkCore;
using Entitys;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

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

using Entitys;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace SyncJob.EntityFrameworkCore
{
    [ConnectionStringName("SourceDb")]
    public interface ISourceDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */

        DbSet<User> Users { get; }
    }
}
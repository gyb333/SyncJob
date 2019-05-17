
using Domain;
using EntityFrameworkCore;
using Entitys;
using Microsoft.EntityFrameworkCore;
using SyncJob;
using System.Data;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EntityFrameworkCore
{
    [ConnectionStringName(TargetDbConsts.ConnectionStringName)]
    public interface ITargetDbContext : IDbContextBase
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */

        DbSet<UserTarget> Users { get; }

        //DbSet<Book> Books { get; }

    }
}
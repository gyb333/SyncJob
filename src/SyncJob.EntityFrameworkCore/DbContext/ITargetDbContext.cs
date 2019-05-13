﻿
using EntityFrameworkCore;
using Entitys;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EntityFrameworkCore
{
    [ConnectionStringName("TargetDb")]
    public interface ITargetDbContext : IDbContextBase
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */

        //DbSet<User> Users { get; }

        DbSet<Book> Books { get; }
 
    }
}
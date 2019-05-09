﻿
using Entitys;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace SyncJob.EntityFrameworkCore
{
    [ConnectionStringName("TargetDb")]
    public class TargetDbContext : AbpDbContext<TargetDbContext>, ITargetDbContext
    {
        public static string TablePrefix { get; set; } = TargetDbConsts.DefaultDbTablePrefix;

        public static string Schema { get; set; } = TargetDbConsts.DefaultDbSchema;

        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        //public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }




        public TargetDbContext(DbContextOptions<TargetDbContext> options) 
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=.\SQLExpress;Database=SchoolDB;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureTargetDb(options =>
            {
                options.TablePrefix = TablePrefix;
                options.Schema = Schema;
            });
        }
    }
}
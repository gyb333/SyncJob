
using Entitys;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace SyncJob.EntityFrameworkCore
{
    [ConnectionStringName("TargetDb")]
    public class TargetDbContext : AbpDbContext<TargetDbContext>, ITargetDbContext
    {
        public static string TablePrefix { get; set; } = Consts.DefaultDbTablePrefix;

        public static string Schema { get; set; } = Consts.DefaultDbSchema;

        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        //public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }




        public TargetDbContext(DbContextOptions<TargetDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureSyncJob(options =>
            {
                options.TablePrefix = TablePrefix;
                options.Schema = Schema;
            });
        }
    }
}
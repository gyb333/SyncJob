
using Entitys;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace SyncJob.EntityFrameworkCore
{
    [ConnectionStringName("SourceDb")]
    public class SourceDbContext : AbpDbContext<SourceDbContext>, ISourceDbContext
    {
        public static string TablePrefix { get; set; } = SourceDbConsts.DefaultDbTablePrefix;

        public static string Schema { get; set; } = SourceDbConsts.DefaultDbSchema;

        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
         public DbSet<User> Users { get; set; }






        public SourceDbContext(DbContextOptions<SourceDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureSourceDb(options =>
            {
                options.TablePrefix = TablePrefix;
                options.Schema = Schema;
            });
        }
    }
}
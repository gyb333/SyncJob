
using System.Data;
using EntityFrameworkCore;
using Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace SyncJob.EntityFrameworkCore
{
    [ConnectionStringName(TargetDbConsts.ConnectionStringName)]
    public class TargetDbContext : AbpDbContext<TargetDbContext>, ITargetDbContext
    {
        public static string TablePrefix { get; set; } = TargetDbConsts.DefaultDbTablePrefix;

        public static string Schema { get; set; } = TargetDbConsts.DefaultDbSchema;

        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        //public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }

        public static string ConnectionString { get; private set; }
        public static DBType dbType { get; private set; }

        public TargetDbContext(DbContextOptions<TargetDbContext> options, IConfigurationRoot configuration) 
            : base(options)
        {
            
            ConnectionString = configuration.GetConnectionString(SourceDbConsts.ConnectionStringName);
            dbType = Database.ProviderName.GetDBType();
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

        public string GetConnectionString()
        {
            return ConnectionString;
        }

        public DBType GetDBType()
        {
            return dbType;
        }

        public IDbConnection CreatConnection()
        {
            return this.CreatDbConnectionDapper(dbType, ConnectionString);

        }
    }
}
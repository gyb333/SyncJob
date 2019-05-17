
using System.Data;
using Domain;
using EntityFrameworkCore;
using Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace SyncJob.EntityFrameworkCore
{
    [ConnectionStringName(SourceDbConsts.ConnectionStringName)]
    public class SourceDbContext : AbpDbContext<SourceDbContext>, ISourceDbContext
    {
        public static string TablePrefix { get; set; } = SourceDbConsts.DefaultDbTablePrefix;

        public static string Schema { get; set; } = SourceDbConsts.DefaultDbSchema;

        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<User> Users { get; set; }


        public static string ConnectionString{get;private set;}
        public static DBType dbType { get; private set; } 


        public SourceDbContext(DbContextOptions<SourceDbContext> options, IConfigurationRoot configuration) 
            : base(options)
        {
            ConnectionString = configuration.GetConnectionString(SourceDbConsts.ConnectionStringName);
            dbType = Database.ProviderName.GetDBType();
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
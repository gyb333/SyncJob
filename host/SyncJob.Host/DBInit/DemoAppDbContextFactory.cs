using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SyncJob.Host
{
    public class DemoAppDbContextFactory : IDesignTimeDbContextFactory<DemoAppDbContext>
    {
        public DemoAppDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<DemoAppDbContext>()
                //.UseSqlServer(configuration.GetConnectionString("TargetDb"));
                .UseMySql(configuration.GetConnectionString("TargetDb"));

            return new DemoAppDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}

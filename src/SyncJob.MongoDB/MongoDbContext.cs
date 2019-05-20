using Domain;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace SyncJob.MongoDB
{
    [ConnectionStringName("SyncJob")]
    public class MongoDbContext : AbpMongoDbContext, IMongoDbContext
    {
        public static string CollectionPrefix { get; set; } = Consts.DefaultDbTablePrefix;

        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureSyncJob(options =>
            {
                options.CollectionPrefix = CollectionPrefix;
            });
        }
    }
}
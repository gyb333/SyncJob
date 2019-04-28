using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace SyncJob.MongoDB
{
    public static class SyncJobMongoDbContextExtensions
    {
        public static void ConfigureSyncJob(
            this IMongoModelBuilder builder,
            Action<Volo.Abp.MongoDB.MongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new MongoModelBuilderConfigOptions();

            optionsAction?.Invoke(options);
        }
    }
}
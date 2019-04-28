using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace SyncJob.MongoDB
{
    public class MongoModelBuilderConfigOptions : MongoModelBuilderConfigurationOptions
    {
        public MongoModelBuilderConfigOptions(
            [NotNull] string tablePrefix = Consts.DefaultDbTablePrefix)
            : base(tablePrefix)
        {
        }
    }
}
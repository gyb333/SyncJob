using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace SyncJob.EntityFrameworkCore
{
    public class ModelBuilderConfigOptions : ModelBuilderConfigurationOptions
    {
        public ModelBuilderConfigOptions(
            [NotNull] string tablePrefix = Consts.DefaultDbTablePrefix,
            [CanBeNull] string schema = Consts.DefaultDbSchema)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}
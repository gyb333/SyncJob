using Domain;
using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EntityFrameworkCore
{
    public class ModelBuilderConfigOptions : ModelBuilderConfigurationOptions
    {
        public ModelBuilderConfigOptions(
            [NotNull] string tablePrefix = TargetDbConsts.DefaultDbTablePrefix,
            [CanBeNull] string schema = TargetDbConsts.DefaultDbSchema)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}
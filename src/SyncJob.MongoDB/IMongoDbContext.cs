using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace SyncJob.MongoDB
{
    [ConnectionStringName("SyncJob")]
    public interface IMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}

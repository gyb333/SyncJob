 
using System.Data;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Study.Common.EFCore
{
 
    public interface IDbContextBase : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */

         
    

        string GetConnectionString();

        DBType GetDBType();

        IDbConnection CreatConnection();
    }
}
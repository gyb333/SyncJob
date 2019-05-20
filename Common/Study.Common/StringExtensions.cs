

namespace Study.Common
{
    public static class StringExtensions
    {
        public static DBType GetDBType(this string ProviderName)
        {
            DBType dbType = DBType.NoSupport;
            if (ProviderName.ToUpper().Contains("SQLServer".ToUpper()))
            {
                dbType = DBType.SQLServer;
            }
            else if (ProviderName.ToUpper().Contains("MySQL".ToUpper()))
            {
                dbType = DBType.MySQL;
            }

            return dbType;
        }
    }
}

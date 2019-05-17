namespace Domain
{
    public static class Consts
    {
        public const string DefaultDbTablePrefix = "SyncJob";

        public const string DefaultDbSchema = null;

        public const int DefaultMaxLength = 128;
 

    }


    public static class SourceDbConsts
    {
        public const string ConnectionStringName = "SourceDb";

        public const string DefaultDbTablePrefix = "";
 
        public const string DefaultDbSchema = null;
    }

    public static class TargetDbConsts
    {
        public const string ConnectionStringName = "MySQLTargetDb";//"TargetDb";//"Hangfire";

        public const string DefaultDbTablePrefix = "";

        public const string DefaultDbSchema = null;
    }

}

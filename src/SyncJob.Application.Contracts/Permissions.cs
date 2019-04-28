namespace SyncJob
{
    public class Permissions
    {
        public const string GroupName = "SyncJob";

        public static string[] GetAll()
        {
            return new[]
            {
                GroupName
            };
        }
    }
}
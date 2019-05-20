using SyncJob.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace SyncJob
{
    public class PermissionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var moduleGroup = context.AddGroup(Permissions.GroupName, L("Permission:SyncJob"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<Resource>(name);
        }
    }
}
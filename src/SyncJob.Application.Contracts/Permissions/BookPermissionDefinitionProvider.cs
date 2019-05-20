using SyncJob.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Permissions
{
    public class BookPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var group = context.AddGroup(BookPermissions.GroupName, L("Permission:Book"));

            var permissions = group.AddPermission(BookPermissions.GroupName, L("Permission:Books"));
            permissions.AddChild(BookPermissions.Update, L("Permission:Edit"));
            permissions.AddChild(BookPermissions.Delete, L("Permission:Delete"));
            permissions.AddChild(BookPermissions.Create, L("Permission:Create"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<Resource>(name);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Reflection;

namespace Permissions
{
    public class BookPermissions
    {
        public const string GroupName = "Books";

        //public static class Products
        //{
        //    public const string Default = GroupName + ".Product";
        //    public const string Delete = Default + ".Delete";
        //    public const string Update = Default + ".Update";
        //    public const string Create = Default + ".Create";

        //}

        //Add your own permission names. Example:
        public const string Create = GroupName + ".Create";
        public const string Delete = GroupName + ".Delete";
        public const string Update = GroupName + ".Update";

        public static string[] GetAll()
        {
            //return Array.Empty<string>();
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(BookPermissions));
        }
    }
}

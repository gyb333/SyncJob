

 
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace Study.Common.EFCore
{
    [Table("`#TempIds`")]
    public class IdEntity<TKey>
    {
        public IdEntity(TKey tKey)
        {
            Id = tKey;


            ////取类上的自定义特性
            //object[] objs = GetType().GetCustomAttributes(typeof(TableAttribute), true);
            //foreach (object obj in objs)
            //{
            //    TableAttribute attr = obj as TableAttribute;
            //    if (attr != null)
            //    {
            //        attr.Name = TableName;
            //        break;
            //    }
            //}
            //PropertyDescriptorCollection attributes = TypeDescriptor.GetProperties(this);
            //Type tableType = typeof(TableAttribute);
            //FieldInfo fieldInfo = tableType.GetField("Name", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance);
            //fieldInfo.SetValue(attributes[tableType], "修改后的文本ID");
        }


        public TKey Id { get; set; }



        public static Type GetIdEntity<TProperty>(string strTableName)
        {
            TypeBuilder typeBuilder = DynamicTypeHelper.BuildType("IdEntity", "Entitys", "SyncJob.Domain");
            //定义构造器参数
            Type[] ctorParams = new Type[] { typeof(string) };
            object[] ctorParamValues = new object[] { $"`#Temp`{strTableName}" };
            typeBuilder.AddAttribute<TableAttribute>(ctorParams, ctorParamValues);
            var id = typeBuilder.AddProperty<TProperty>("Id");
            typeBuilder.AddCtor(new Type[] { typeof(TProperty) }, new FieldBuilder[] { id });
            //return typeBuilder.CreateType();
            return null;
        }
    }
 
}

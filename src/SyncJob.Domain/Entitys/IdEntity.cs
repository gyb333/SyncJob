

using Entitys.Table;

namespace Entitys
{
    [Table("`#TempIds`")]
    public class IdEntity<TKey> 
    {
        public IdEntity(TKey tKey )
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


        public TKey Id { get ; set ; }
        
    }
}

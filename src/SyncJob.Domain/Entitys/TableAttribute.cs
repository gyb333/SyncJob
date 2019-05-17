using System;
using System.Collections.Generic;
using System.Text;

namespace Entitys.Table
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TableAttribute : Attribute
    {
        //
        // 摘要:
        //     Initializes a new instance of the System.ComponentModel.DataAnnotations.Schema.TableAttribute
        //     class using the specified name of the table.
        //
        // 参数:
        //   name:
        //     The name of the table the class is mapped to.
        public TableAttribute(string name)
        {

        }

        //
        // 摘要:
        //     Gets the name of the table the class is mapped to.
        //
        // 返回结果:
        //     The name of the table the class is mapped to.
        public string Name { get; set; }
        //
        // 摘要:
        //     Gets or sets the schema of the table the class is mapped to.
        //
        // 返回结果:
        //     The schema of the table the class is mapped to.
        public string Schema { get; set; }
    }
}

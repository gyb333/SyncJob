using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DbContext
{
    public class DynamicTypeHelper
    {
        public static Assembly NewAssembly()

        {

            //创建编译器实例。 

            provider = new CSharpCodeProvider();

            //设置编译参数。 

            cp = new CompilerParameters();

            cp.GenerateExecutable = false;

            cp.GenerateInMemory = true;

            // Generate an executable instead of

            // a class library.

            //cp.GenerateExecutable = true;

            // Set the assembly file name to generate.

            cp.OutputAssembly = "c:\\1.dll";

            // Generate debug information.

            cp.IncludeDebugInformation = true;

            // Save the assembly as a physical file.

            cp.GenerateInMemory = false;

            // Set the level at which the compiler

            // should start displaying warnings.

            cp.WarningLevel = 3;

            // Set whether to treat all warnings as errors.

            cp.TreatWarningsAsErrors = false;

            // Set compiler argument to optimize output.

            cp.CompilerOptions = "/optimize";

            cp.ReferencedAssemblies.Add("System.dll");

            //cp.ReferencedAssemblies.Add("System.Core.dll");

            cp.ReferencedAssemblies.Add("System.Data.dll");

            //cp.ReferencedAssemblies.Add("System.Data.DataSetExtensions.dll");

            cp.ReferencedAssemblies.Add("System.Deployment.dll");

            cp.ReferencedAssemblies.Add("System.Design.dll");

            cp.ReferencedAssemblies.Add("System.Drawing.dll");

            cp.ReferencedAssemblies.Add("System.Windows.Forms.dll");

            //创建动态代码。 

            StringBuilder classSource = new StringBuilder();

            classSource.Append("using System;using System.Windows.Forms;\npublic  class  DynamicClass: UserControl \n");

            classSource.Append("{\n");

            classSource.Append("public DynamicClass()\n{\nInitializeComponent();\nConsole.WriteLine(\"hello\");}\n");

            classSource.Append("private System.ComponentModel.IContainer components = null;\nprotected override void Dispose(bool disposing)\n{\n");

            classSource.Append("if (disposing && (components != null)){components.Dispose();}base.Dispose(disposing);\n}\n");

            classSource.Append("private void InitializeComponent(){\nthis.SuspendLayout();this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);");

            classSource.Append("this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;this.Name = \"DynamicClass\";this.Size = new System.Drawing.Size(112, 74);this.ResumeLayout(false);\n}");

            //创建属性。 

            /*************************在这里改成需要的属性******************************/

            classSource.Append(propertyString("aaa"));

            classSource.Append(propertyString("bbb"));

            classSource.Append(propertyString("ccc"));

            classSource.Append("}");

            System.Diagnostics.Debug.WriteLine(classSource.ToString());

            //编译代码。 

            CompilerResults result = provider.CompileAssemblyFromSource(cp, classSource.ToString());

            if (result.Errors.Count > 0)

            {

                for (int i = 0; i < result.Errors.Count; i++)

                    Console.WriteLine(result.Errors[i]);

                Console.WriteLine("error");

                return null;

            }

            //获取编译后的程序集。 

            Assembly assembly = result.CompiledAssembly;

            return assembly;

        }

        private static string propertyString(string propertyName)

        {

            StringBuilder sbProperty = new StringBuilder();

            sbProperty.Append(" private  int  _" + propertyName + "  =  0;\n");

            sbProperty.Append(" public  int  " + "" + propertyName + "\n");

            sbProperty.Append(" {\n");

            sbProperty.Append(" get{  return  _" + propertyName + ";}  \n");

            sbProperty.Append(" set{  _" + propertyName + "  =  value;  }\n");

            sbProperty.Append(" }");

            return sbProperty.ToString();

        }
    }
}

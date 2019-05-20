

using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Study.Common
{
    public static class DynamicTypeHelper
    {
        public static TypeBuilder BuildType(string strTypeName, string strModuleName = "Study", string strAssemblyName = "GYB")
        {
            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = strAssemblyName;

            //动态创建一个程序集
            //AppDomain currentDomain = Thread.GetDomain();
            //AssemblyBuilder myAsmBuilder = currentDomain.DefineDynamicAssembly(myAsmName, AssemblyBuilderAccess.Run);
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);

            //动态创建一个模块
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(strModuleName);

            //动态创建一个类型：MyType
            TypeBuilder typeBuilder = moduleBuilder.DefineType(strTypeName, TypeAttributes.Public);

            //定义构造器参数
            Type[] ctorParams = new Type[] { typeof(string) };
            object[] ctorParamValues = new object[] { "attribute value" };
            //typeBuilder.AddAttribute<ClassCreatorAttribute>(ctorParams, ctorParamValues);

            //var id = typeBuilder.AddProperty<int>("Id");
            //var name = typeBuilder.AddProperty<string>("Name");
            //var count = typeBuilder.AddField<int>("count");

            //typeBuilder.AddCtor(new Type[] { typeof(int), typeof(string) }, new FieldBuilder[] { id, name });
            //typeBuilder.AddToString("ToString", new FieldBuilder[] { id, name });
            //// save assembly    

            //assemblyBuilder.Save("Pets.dll");

            return typeBuilder;
        }

        public static void AddAttribute<TAttribute>(this TypeBuilder typeBuilder, Type[] ctorParams, object[] ctorParamValues)
            where TAttribute : Attribute
        {

            //获取构造器信息
            ConstructorInfo classCtorInfo = typeof(TAttribute).GetConstructor(ctorParams);

            //动态创建ClassCreatorAttribute
            CustomAttributeBuilder builder = new CustomAttributeBuilder(
                           classCtorInfo, ctorParamValues //new object[] { "Joe Programmer" }
            );

            //将上面动态创建的Attribute附加到(动态创建的)类型MyType
            typeBuilder.SetCustomAttribute(builder);
        }

        public static FieldBuilder AddField<TProperty>(this TypeBuilder typeBuilder, string strFieldName)
        {
            Type type = typeof(TProperty);
            //var defaultValue = type.IsValueType ? Activator.CreateInstance(type) : null;
            return typeBuilder.DefineField(strFieldName.ToLower().Trim(), type, FieldAttributes.Private);
        }
        public static FieldBuilder AddProperty<TProperty>(this TypeBuilder typeBuilder, string strPropertyName, bool isNeedGet = true, bool isNeedSet = true)
        {
            strPropertyName = strPropertyName.Trim();
            Type type = typeof(TProperty);

            //FieldBuilder field = typeBuilder.DefineField(strPropertyName.ToLower().Trim(), type, FieldAttributes.Private);
            FieldBuilder field = typeBuilder.AddField<TProperty>(strPropertyName);
             

            if (isNeedGet || isNeedSet)
            {
                PropertyBuilder property = typeBuilder.DefineProperty(strPropertyName, PropertyAttributes.HasDefault, type, null);
                MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

                if (isNeedGet)
                {
                    MethodBuilder getMethod = typeBuilder.DefineMethod($"Get{strPropertyName}", getSetAttr, type, Type.EmptyTypes);
                    ILGenerator nameGetIL = getMethod.GetILGenerator();
                    nameGetIL.Emit(OpCodes.Ldarg_0);
                    nameGetIL.Emit(OpCodes.Ldfld, field);
                    nameGetIL.Emit(OpCodes.Ret);
                    property.SetGetMethod(getMethod);
                }

                if (isNeedSet)
                {
                    MethodBuilder setMethod = typeBuilder.DefineMethod($"Set{strPropertyName}", getSetAttr, null, new Type[] { type });
                    ILGenerator nameSetIL = setMethod.GetILGenerator();
                    nameSetIL.Emit(OpCodes.Ldarg_0);
                    nameSetIL.Emit(OpCodes.Ldarg_1);
                    nameSetIL.Emit(OpCodes.Stfld, field);
                    nameSetIL.Emit(OpCodes.Ret);
                    property.SetSetMethod(setMethod);
                }
            }

            return field;


        }

        

        public static OpCode GetOpCode(int i)
        {
            OpCode opCode;
            switch (i)
            {
                case 0:
                    opCode = OpCodes.Ldarg_0;
                    break;
                case 1:
                    opCode = OpCodes.Ldarg_1;
                    break;
                case 2:
                    opCode = OpCodes.Ldarg_2;
                    break;
                case 3:
                    opCode = OpCodes.Ldarg_3;
                    break;
                default:
                    opCode = OpCodes.Ldarg;
                    break;
            }
            return opCode;
        }

        public static void AddCtor(this TypeBuilder typeBuilder, Type[] constructorArgs, FieldBuilder[] fields)
        {
            Type objType = Type.GetType("System.Object");
            ConstructorInfo objCtor = objType.GetConstructor(new Type[0]);//基类构造函数

            //Type[] constructorArgs = { typeof(int), typeof(string) };

            var constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, constructorArgs);
            ILGenerator generator = constructorBuilder.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0);// this
            generator.Emit(OpCodes.Call, objCtor);


            for (int i = 0; i < constructorArgs.Length; i++)
            {
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(GetOpCode(i + 1));//OpCodes.Ldarg_1
                generator.Emit(OpCodes.Stfld, fields[i]);//字段名称
            }

            generator.Emit(OpCodes.Ret);
        }
        public static void AddToString(this TypeBuilder typeBuilder, string strMethodName, FieldBuilder[] fields)
        {
            var method = typeBuilder.DefineMethod(strMethodName, MethodAttributes.Virtual | MethodAttributes.Public, typeof(string), null);

            var generator = method.GetILGenerator();
            var local = generator.DeclareLocal(typeof(string)); // create a local variable
            generator.Emit(OpCodes.Ldstr, "Id:[{0}], Name:[{1}]");

            foreach (var each in fields)
            {
                generator.Emit(OpCodes.Ldarg_0); // this
                generator.Emit(OpCodes.Ldfld, each);
                if (each.FieldType.IsValueType)
                    generator.Emit(OpCodes.Box, each.FieldType); // 转换值类型为对象类型装箱操作
            }
            generator.Emit(OpCodes.Call, typeof(string).GetMethod("Format",
              new Type[] { typeof(string), typeof(object), typeof(object) }));
            generator.Emit(OpCodes.Stloc, local); // set local variable
            generator.Emit(OpCodes.Ldloc, local); // load local variable to stack
            generator.Emit(OpCodes.Ret);
        }

    }
}

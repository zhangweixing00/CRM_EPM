using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;

namespace Founder.PKURG.EPM.Common
{
    public class CustomType
    {
        public Object GetInstance(Type type, Dictionary<string, string> dict)
        {
            object instance = Activator.CreateInstance(type);

            foreach (KeyValuePair<string, string> item in dict)
            {
                PropertyInfo info = type.GetProperty(item.Key);
                if (info != null)
                {
                    info.SetValue(instance, item.Value, null);
                }
            }

            return instance;
        }

        public Type CreateType(List<string> properyNames)
        {
            AssemblyName aName = new AssemblyName("DynamicAssembly");
            AssemblyBuilder ab =
                AppDomain.CurrentDomain.DefineDynamicAssembly(
                    aName,
                    AssemblyBuilderAccess.RunAndSave);

            // For a single-module assembly, the module name is usually
            // the assembly name plus an extension.
            ModuleBuilder mb =
                ab.DefineDynamicModule(aName.Name, aName.Name + ".dll");

            TypeBuilder tb = mb.DefineType(
                "MyDynamicType",
                 TypeAttributes.Public);

            foreach (string item in properyNames)
            {
                AddPropery(tb, item);
            }
            return tb.CreateType();
        }
        
        public void AddPropery(TypeBuilder builder, string name)
        {
            FieldBuilder field = builder.DefineField("_" + name, typeof(string), FieldAttributes.Private);

            PropertyBuilder propery = builder.DefineProperty(name, PropertyAttributes.HasDefault, typeof(string), null);

            // The property "set" and property "get" methods require a special
            // set of attributes.
            MethodAttributes getSetAttr = MethodAttributes.Public |
                MethodAttributes.SpecialName | MethodAttributes.HideBySig;

            // Define the "get" accessor method for Number. The method returns
            // an integer and has no arguments. (Note that null could be 
            // used instead of Types.EmptyTypes)
            MethodBuilder getAccessor = builder.DefineMethod(
                "get_" + name,
                getSetAttr,
                typeof(string),
                Type.EmptyTypes);

            ILGenerator getIL = getAccessor.GetILGenerator();
            // For an instance property, argument zero is the instance. Load the 
            // instance, then load the private field and return, leaving the
            // field value on the stack.
            getIL.Emit(OpCodes.Ldarg_0);
            getIL.Emit(OpCodes.Ldfld, field);
            getIL.Emit(OpCodes.Ret);

            // Define the "set" accessor method for Number, which has no return
            // type and takes one argument of type int (Int32).
            MethodBuilder setAccessor = builder.DefineMethod(
                "set_" + name, getSetAttr, null,
                new Type[] { typeof(string) });

            ILGenerator numberSetIL = setAccessor.GetILGenerator();
            // Load the instance and then the numeric argument, then store the
            // argument in the field.
            numberSetIL.Emit(OpCodes.Ldarg_0);
            numberSetIL.Emit(OpCodes.Ldarg_1);
            numberSetIL.Emit(OpCodes.Stfld, field);
            numberSetIL.Emit(OpCodes.Ret);

            // Last, map the "get" and "set" accessor methods to the 
            // PropertyBuilder. The property is now complete. 
            propery.SetGetMethod(getAccessor);
            propery.SetSetMethod(setAccessor);
        }
    }
}

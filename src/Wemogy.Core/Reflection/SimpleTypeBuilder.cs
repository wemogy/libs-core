using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Wemogy.Core.Reflection
{
    public class SimpleTypeBuilder
    {
        private readonly TypeBuilder _typeBuilder;

        public SimpleTypeBuilder(
            string typeName,
            Type? parentType = null,
            List<Type>? interfaces = null,
            string assemblyName = "SimpleTypeBuilderAssembly",
            string moduleName = "SimpleTypeBuilderModule")
        {
            // 1. create assembly name
            var actualAssemblyName = new AssemblyName(assemblyName);

            // 2. create the assembly builder
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                actualAssemblyName,
                AssemblyBuilderAccess.Run);

            // 3. that is needed to create a module builder
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(moduleName);

            // 4. and finally our TypeBuilder (a public class)
            _typeBuilder = moduleBuilder.DefineType(
                typeName,
                TypeAttributes.Public | TypeAttributes.Class,
                parentType,
                interfaces?.ToArray());
        }

        public SimpleTypeBuilder AddProperty(string propertyName, Type propertyType, PropertyInfo propertyInfo)
        {
            var fieldBuilder = _typeBuilder.DefineField(
                "_" + propertyName,
                propertyType,
                FieldAttributes.Private); // creates the backing field
            var propertyBuilder = _typeBuilder.DefineProperty(
                propertyName,
                PropertyAttributes.HasDefault,
                propertyType,
                null);
            var getSetAttribute = MethodAttributes.Public |
                                MethodAttributes.HideBySig |
                                MethodAttributes.SpecialName |
                                MethodAttributes.Virtual;

            // get method
            var getPropertyMethodBuilder = _typeBuilder.DefineMethod(
                "get_" + propertyName,
                getSetAttribute, /*returnType*/
                propertyType, /*parameter types*/
                Type.EmptyTypes); // see IL for the right MethodAttributes
            var getIl = getPropertyMethodBuilder.GetILGenerator();

            // create the code in the get method
            getIl.Emit(OpCodes.Ldarg_0); // this
            getIl.Emit(
                OpCodes.Ldfld,
                fieldBuilder); // backingfield
            getIl.Emit(OpCodes.Ret);

            // set method
            var setPropertyMethodBuilder =
                _typeBuilder.DefineMethod(
                    "set_" + propertyName,
                    getSetAttribute, /*returnType*/
                    null, /*parameter types*/
                    new[] { propertyType }); // see IL for the right MethodAttributes

            var setIl = setPropertyMethodBuilder.GetILGenerator();

            // create the code in the set method
            setIl.Emit(OpCodes.Ldarg_0); // this
            setIl.Emit(OpCodes.Ldarg_1); // 'value'
            setIl.Emit(
                OpCodes.Stfld,
                fieldBuilder); // backing field

            setIl.Emit(OpCodes.Nop);
            setIl.Emit(OpCodes.Ret);

            // add methods to the propertyBuilder
            propertyBuilder.SetGetMethod(getPropertyMethodBuilder);
            propertyBuilder.SetSetMethod(setPropertyMethodBuilder);

            var customAttributes = propertyInfo.GetCustomAttributes(true);
            foreach (var customAttribute in customAttributes)
            {
                var attributeBuilder = BuildCustomAttribute((Attribute)customAttribute);
                propertyBuilder.SetCustomAttribute(attributeBuilder);
            }

            return this;
        }

        public Type CreateType()
        {
            return _typeBuilder.CreateTypeInfo().AsType();
        }

        private CustomAttributeBuilder BuildCustomAttribute(Attribute attribute)
        {
            var type = attribute.GetType();

            var constructor = type.GetConstructor(Type.EmptyTypes);
            return new CustomAttributeBuilder(
                constructor,
                Array.Empty<object>());
        }
    }
}

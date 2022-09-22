using System;
using System.Collections.Generic;
using System.Linq;

namespace Wemogy.Core.Reflection
{
    public class TypeEditor
    {
        private readonly Type _originalType;
        private readonly Dictionary<string, Type> _propertyTypeModifications;
        private readonly List<Type> _interfaces;

        public TypeEditor(Type originalType)
        {
            _originalType = originalType;
            _propertyTypeModifications = new Dictionary<string, Type>();
            _interfaces = new List<Type>();
        }

        public TypeEditor ModifyPropertyType(string propertyName, Type modifiedType)
        {
            _propertyTypeModifications.Add(propertyName, modifiedType);
            return this;
        }

        public TypeEditor AddInterface(Type interfaceType)
        {
            _interfaces.Add(interfaceType);
            return this;
        }

        public Type CreateType(string typeName)
        {
            var simpleTypeBuilder = new SimpleTypeBuilder(
                typeName,
                null,
                _interfaces.ToList());
            var originalProperties = _originalType.GetProperties();

            foreach (var originalProperty in originalProperties)
            {
                var propertyType = _propertyTypeModifications.ContainsKey(originalProperty.Name)
                    ? _propertyTypeModifications[originalProperty.Name]
                    : originalProperty.PropertyType;

                simpleTypeBuilder.AddProperty(originalProperty.Name, propertyType, originalProperty);
            }

            return simpleTypeBuilder.CreateType();
        }
    }
}

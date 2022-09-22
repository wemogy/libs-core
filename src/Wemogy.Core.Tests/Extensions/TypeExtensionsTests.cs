using System;
using FluentAssertions;
using Wemogy.Core.Extensions;
using Wemogy.Core.Tests.Extensions.AssemblyExtensionsTestData;
using Xunit;

namespace Wemogy.Core.Tests.Extensions;

public class TypeExtensionsTests
{
    [Theory]
    [InlineData(typeof(InterfaceImplementation1), typeof(IInterface), true, typeof(IInterface))]
    [InlineData(typeof(GenericInterfaceImplementation1), typeof(IGenericInterface<float>), false, null)]
    [InlineData(typeof(GenericInterfaceImplementation1), typeof(IGenericInterface<int>), true, typeof(IGenericInterface<int>))]
    [InlineData(typeof(GenericInterfaceImplementation1), typeof(IGenericInterface<>), true, typeof(IGenericInterface<int>))]
    public void InheritsOrImplements_ShouldWork(Type typeToCheck, Type genericType, bool expectedToInheritOrImplement, Type? expectedBaseType)
    {
        // Arrange

        // Act
        var result = typeToCheck.InheritsOrImplements(genericType, out Type? baseType);

        // Assert
        result.Should().Be(expectedToInheritOrImplement);
        baseType.Should().Be(expectedBaseType);
    }
}

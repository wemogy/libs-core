using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Wemogy.Core.Extensions;
using Wemogy.Core.Tests.Extensions.AssemblyExtensionsTestData;
using Xunit;

namespace Wemogy.Core.Tests.Extensions;

public class AssemblyExtensionsTests
{
    [Theory]
    [InlineData(typeof(IInterface), 3)]
    [InlineData(typeof(IGenericInterface<>), 3)]
    public void GetClassTypesWhichImplementInterface_ShouldWork(Type interfaceType, int expectedImplementationsCount)
    {
        // Arrange
        var assemblies = new List<Assembly>()
        {
            Assembly.GetExecutingAssembly()
        };

        // Act
        var classTypes = assemblies.GetClassTypesWhichImplementInterface(interfaceType);

        // Assert
        classTypes.Should().HaveCount(expectedImplementationsCount);
    }
}

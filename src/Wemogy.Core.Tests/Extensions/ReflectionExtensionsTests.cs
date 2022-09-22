using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Wemogy.Core.Extensions;
using Xunit;

namespace Wemogy.Core.Tests.Extensions
{
    public class ReflectionExtensionsTests
    {
        [Fact]
        public void ResolvePropertyTypeOfPropertyPath_ShouldWork()
        {
            // Arrange
            var type = typeof(TestModelType);

            // Act
            var resolvedFirstLevelPropertyType = type.ResolvePropertyTypeOfPropertyPath("/levelRoles/0");
            var resolvedSecondsLevelPropertyType = type.ResolvePropertyTypeOfPropertyPath("/levelRoles/0/2");

            var resolvedFirstLevelPropertyTypeList = type.ResolvePropertyTypeOfPropertyPath("/roles/0");
            var resolvedSecondsLevelPropertyTypeList = type.ResolvePropertyTypeOfPropertyPath("/roles/0/2");

            // Assert
            Assert.Equal(typeof(long[]), resolvedFirstLevelPropertyType);
            Assert.Equal(typeof(long), resolvedSecondsLevelPropertyType);

            Assert.Equal(typeof(List<long>), resolvedFirstLevelPropertyTypeList);
            Assert.Equal(typeof(long), resolvedSecondsLevelPropertyTypeList);
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Just a test")]
    class TestModelType
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public long[][] LevelRoles { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public List<List<long>> Roles { get; set; }

        public TestModelType()
        {
            LevelRoles = Array.Empty<long[]>();
            Roles = new List<List<long>>();
        }
    }
}

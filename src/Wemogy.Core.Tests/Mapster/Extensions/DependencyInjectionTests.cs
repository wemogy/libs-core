using FluentAssertions;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Wemogy.Core.Mapster.Extensions;
using Wemogy.Core.Tests.Mapster.TestResources.Models;
using Xunit;

namespace Wemogy.Core.Tests.Mapster.Extensions;

public class DependencyInjectionTests
{
    [Fact]
    public void AddMapster_ShouldEnableExplicitMapping()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddMapster();
        var user = new User();
        var member = new Member();

        // Act
        var exception = Record.Exception(() => user.Adapt<Folder>());
        var validMappingException = Record.Exception(() => user.Adapt<Member>());

        // reverse mapping should not work automatically
        var reverseMappingException = Record.Exception(() => member.Adapt<User>());

        // Assert
        exception.Should().NotBeNull();
        validMappingException.Should().BeNull();
        reverseMappingException.Should().NotBeNull();
    }
}

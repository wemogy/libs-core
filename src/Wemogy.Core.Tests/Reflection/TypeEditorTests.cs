using System;
using FluentAssertions;
using Wemogy.Core.Reflection;
using Wemogy.Core.Tests.Reflection.TypeEditorTestData;
using Xunit;

namespace Wemogy.Core.Tests.Reflection;

public class TypeEditorTests
{
    [Fact]
    public void TypeEditor_ShouldModifyTypesCorrectly()
    {
        // Arrange
        var typeEditor = new TypeEditor(typeof(Animal));

        // Act
        typeEditor
            .ModifyPropertyType(
                nameof(Animal.Id),
                typeof(string));
        typeEditor.AddInterface(typeof(IEntity<string>));
        var modifiedType = typeEditor.CreateType("ModifiedAnimal");

        // Assert
        modifiedType.Should().BeDerivedFrom<object>();
        modifiedType.Should().Implement<IEntity<string>>();
        modifiedType.Should().HaveProperty<string>(nameof(Animal.Id));
        modifiedType.Should().HaveProperty<string>(nameof(Animal.Name));
        modifiedType.Should().HaveProperty<Animal>(nameof(Animal.BestFriend))
            .Which.PropertyType.Should().HaveProperty<Guid>(nameof(Animal.Id));
        modifiedType.Should().HaveProperty<string>(nameof(Animal.ZooId))
            .Which.GetCustomAttributes(true).Should().ContainItemsAssignableTo<DummyAttribute>();
    }
}

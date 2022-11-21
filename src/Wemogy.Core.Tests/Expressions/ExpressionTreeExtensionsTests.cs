using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using Wemogy.Core.Expressions;
using Wemogy.Core.Extensions;
using Wemogy.Core.Tests.Expressions.TestingData.Models;
using Xunit;

namespace Wemogy.Core.Tests.Expressions;

public class ExpressionTreeExtensionsTests
{
    [Fact]
    public void ModifyPropertyValueEqual_ShouldWork()
    {
        // Arrange
        var files = WindowsFile.Faker.Generate(3);
        var modifiedFiles = files.Clone();
        modifiedFiles.ForEach(x => x.Name += "Modified");
        var firstFileName = files.First().Name;
        Expression<Func<WindowsFile, bool>> expression = x => x.Name == firstFileName;

        // Act
        var modifiedExpression = expression.ModifyPropertyValue(nameof(WindowsFile.Name), x => $"{x}Modified");
        var filesResult = files.Where(modifiedExpression.Compile()).ToList();
        var modifiedFilesResult = modifiedFiles.Where(modifiedExpression.Compile()).ToList();

        // Assert
        filesResult.Should().BeEmpty();
        modifiedFilesResult.Should().HaveCount(1);
        modifiedFilesResult.Should().Contain(modifiedFiles.First());
    }

    [Fact]
    public void ModifyPropertyValueEqualReverse_ShouldWork()
    {
        // Arrange
        var files = WindowsFile.Faker.Generate(3);
        var modifiedFiles = files.Clone();
        modifiedFiles.ForEach(x => x.Name += "Modified");
        var firstFileName = files.First().Name;
        Expression<Func<WindowsFile, bool>> expression = x => firstFileName == x.Name;

        // Act
        var modifiedExpression = expression.ModifyPropertyValue(nameof(WindowsFile.Name), x => $"{x}Modified");
        var filesResult = files.Where(modifiedExpression.Compile()).ToList();
        var modifiedFilesResult = modifiedFiles.Where(modifiedExpression.Compile()).ToList();

        // Assert
        filesResult.Should().BeEmpty();
        modifiedFilesResult.Should().HaveCount(1);
        modifiedFilesResult.Should().Contain(modifiedFiles.First());
    }

    [Fact]
    public void ModifyPropertyValueUnequal_ShouldWork()
    {
        // Arrange
        var files = WindowsFile.Faker.Generate(3);
        var modifiedFiles = files.Clone();
        modifiedFiles.ForEach(x => x.Name += "Modified");
        var firstFileName = files.First().Name;
        Expression<Func<WindowsFile, bool>> expression = x => x.Name != firstFileName;

        // Act
        var modifiedExpression = expression.ModifyPropertyValue(nameof(WindowsFile.Name), x => $"{x}Modified");
        var filesResult = files.Where(modifiedExpression.Compile()).ToList();
        var modifiedFilesResult = modifiedFiles.Where(modifiedExpression.Compile()).ToList();

        // Assert
        filesResult.Should().HaveCount(3);
        modifiedFilesResult.Should().HaveCount(2);
        modifiedFilesResult.Should().NotContain(modifiedFiles.First());
    }

    [Fact]
    public void ModifyPropertyValueUnequalReverse_ShouldWork()
    {
        // Arrange
        var files = WindowsFile.Faker.Generate(3);
        var modifiedFiles = files.Clone();
        modifiedFiles.ForEach(x => x.Name += "Modified");
        var firstFileName = files.First().Name;
        Expression<Func<WindowsFile, bool>> expression = x => firstFileName != x.Name;

        // Act
        var modifiedExpression = expression.ModifyPropertyValue(nameof(WindowsFile.Name), x => $"{x}Modified");
        var filesResult = files.Where(modifiedExpression.Compile()).ToList();
        var modifiedFilesResult = modifiedFiles.Where(modifiedExpression.Compile()).ToList();

        // Assert
        filesResult.Should().HaveCount(3);
        modifiedFilesResult.Should().HaveCount(2);
        modifiedFilesResult.Should().NotContain(modifiedFiles.First());
    }

    [Fact]
    public void ModifyPropertyValueContain_ShouldWork()
    {
        // Arrange
        var files = WindowsFile.Faker.Generate(3);
        var modifiedFiles = files.Clone();
        modifiedFiles.ForEach(x => x.Name += "Modified");
        var fileNames = new List<string>() { files.First().Name };
        Expression<Func<WindowsFile, bool>> expression = x => fileNames.Contains(x.Name);

        // Act
        var modifiedExpression = expression.ModifyPropertyValue(nameof(WindowsFile.Name), x => $"{x}Modified");
        var filesResult = files.Where(modifiedExpression.Compile()).ToList();
        var modifiedFilesResult = modifiedFiles.Where(modifiedExpression.Compile()).ToList();

        // Assert
        filesResult.Should().BeEmpty();
        modifiedFilesResult.Should().HaveCount(1);
        modifiedFilesResult.Should().Contain(modifiedFiles.First());
    }

    [Fact]
    public void ModifyPropertyValueContainOther_ShouldWork()
    {
        // Arrange
        var files = WindowsFile.Faker.Generate(3);
        var modifiedFiles = files.Clone();
        modifiedFiles.ForEach(x => x.Name += "Modified");
        var fileIds = new List<Guid>() { files.First().Id };
        Expression<Func<WindowsFile, bool>> expression = x => fileIds.Contains(x.Id);

        // Act
        var modifiedExpression = expression.ModifyPropertyValue(nameof(WindowsFile.Name), x => $"{x}Modified");
        var filesResult = files.Where(modifiedExpression.Compile()).ToList();
        var modifiedFilesResult = modifiedFiles.Where(modifiedExpression.Compile()).ToList();

        // Assert
        filesResult.Should().HaveCount(1);
        modifiedFilesResult.Should().HaveCount(1);
        modifiedFilesResult.Should().Contain(modifiedFiles.First());
    }

    [Fact]
    public void ModifyPropertyValueMixed_ShouldWork()
    {
        // Arrange
        var files = WindowsFile.Faker.Generate(3);
        var modifiedFiles = files.Clone();
        modifiedFiles.ForEach(x => x.Name += "Modified");
        var fileNames = new List<string>() { files.First().Name };
        var secondFileName = files[1].Name;
        Expression<Func<WindowsFile, bool>> expression = x => fileNames.Contains(x.Name) || x.Name == secondFileName;

        // Act
        var modifiedExpression = expression.ModifyPropertyValue(nameof(WindowsFile.Name), x => $"{x}Modified");
        var filesResult = files.Where(modifiedExpression.Compile()).ToList();
        var modifiedFilesResult = modifiedFiles.Where(modifiedExpression.Compile()).ToList();

        // Assert
        filesResult.Should().BeEmpty();
        modifiedFilesResult.Should().HaveCount(2);
        modifiedFilesResult.Should().Contain(modifiedFiles.First());
        modifiedFilesResult.Should().Contain(modifiedFiles[1]);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using Mapster;
using Wemogy.Core.Expressions;
using Wemogy.Core.Tests.Expressions.TestingData.Models;
using Xunit;

namespace Wemogy.Core.Tests.Expressions;

public class ExpressionTreeConverterTests
{
    [Fact]
    public void ReplaceFunctionalBinaryExpressionParameterType1()
    {
        // Arrange
        var linuxFiles = LinuxFile.Faker.Generate(2); // LinuxFile is autogenerated based on WindowsFile
        var windowsFiles = linuxFiles.Adapt<List<WindowsFile>>(new TypeAdapterConfig());
        var firstFileId = windowsFiles[0].Id;

        Expression<Func<WindowsFile, bool>> windowsExpression = x => x.Id == firstFileId;

        // Act
        Expression<Func<LinuxFile, bool>> linuxExpression = windowsExpression
            .ReplaceFunctionalBinaryExpressionParameterType<WindowsFile, LinuxFile>();

        // Assert
        var filteredWindowsFiles = windowsFiles.Where(windowsExpression.Compile()).ToList();
        var filteredLinuxFiles = linuxFiles.Where(linuxExpression.Compile()).ToList();
        filteredWindowsFiles.Should().HaveCount(1);
        filteredLinuxFiles.Should().HaveCount(1);
        filteredWindowsFiles[0].Id.Should().Be(Guid.Parse(filteredLinuxFiles[0].Id));
    }

    [Fact]
    public void ReplaceFunctionalBinaryExpressionParameterType2()
    {
        // Arrange
        var linuxFiles = LinuxFile.Faker.Generate(2); // LinuxFile is autogenerated based on WindowsFile
        var windowsFiles = linuxFiles.Adapt<List<WindowsFile>>(new TypeAdapterConfig());
        var prefix = "tenantAStageProd";
        linuxFiles.ForEach(x => x.Id = $"{prefix}{x.Id}");

        var firstFileId = windowsFiles[0].Id;

        Expression<Func<WindowsFile, bool>> windowsExpression = x => x.Id == firstFileId;

        // Act
        Expression<Func<LinuxFile, bool>> linuxExpression = windowsExpression
            .ReplaceFunctionalBinaryExpressionParameterType<WindowsFile, LinuxFile>(prefix);

        // x => x.Id == firstFileId;

        // Assert
        var filteredLinuxFiles = linuxFiles.Where(linuxExpression.Compile()).ToList();
        var filteredWindowsFiles = windowsFiles.Where(windowsExpression.Compile()).ToList();
        filteredLinuxFiles.Should().HaveCount(1);
        filteredWindowsFiles.Should().HaveCount(1);
        filteredLinuxFiles[0].Id.Should().EndWith(filteredWindowsFiles[0].Id.ToString());
    }

    [Fact]
    public void ReplaceFunctionalBinaryExpressionParameterType3()
    {
        // Arrange
        var linuxFiles = LinuxFile.Faker.Generate(2); // LinuxFile is autogenerated based on WindowsFile
        var windowsFiles = linuxFiles.Adapt<List<WindowsFile>>(new TypeAdapterConfig());
        var prefix = "tenantAStageProd";
        linuxFiles.ForEach(x => x.Id = $"{prefix}{x.Id}");

        var firstFileName = windowsFiles[0].Name;

        Expression<Func<WindowsFile, bool>> windowsExpression = x => x.Name == firstFileName;

        // Act
        Expression<Func<LinuxFile, bool>> linuxExpression = windowsExpression
            .ReplaceFunctionalBinaryExpressionParameterType<WindowsFile, LinuxFile>(prefix);

        // x => x.Id == firstFileId;

        // Assert
        var filteredLinuxFiles = linuxFiles.Where(linuxExpression.Compile()).ToList();
        var filteredWindowsFiles = windowsFiles.Where(windowsExpression.Compile()).ToList();
        filteredLinuxFiles.Should().HaveCount(1);
        filteredWindowsFiles.Should().HaveCount(1);
        filteredLinuxFiles[0].Id.Should().EndWith(filteredWindowsFiles[0].Id.ToString());
    }

    [Fact]
    public void ReplaceFunctionalBinaryExpressionParameterType4()
    {
        // Arrange
        var linuxFiles = LinuxFile.Faker.Generate(2); // LinuxFile is autogenerated based on WindowsFile
        var windowsFiles = linuxFiles.Adapt<List<WindowsFile>>(new TypeAdapterConfig());
        var prefix = "tenantAStageProd";
        linuxFiles.ForEach(x => x.Id = $"{prefix}{x.Id}");

        var firstFileId = windowsFiles[0].Id;
        var firstFileName = windowsFiles[0].Name;

        Expression<Func<WindowsFile, bool>> windowsExpression = x => x.Id == firstFileId && x.Name == firstFileName;

        // Act
        Expression<Func<LinuxFile, bool>> linuxExpression = windowsExpression
            .ReplaceFunctionalBinaryExpressionParameterType<WindowsFile, LinuxFile>(prefix);

        // Assert
        var filteredLinuxFiles = linuxFiles.Where(linuxExpression.Compile()).ToList();
        var filteredWindowsFiles = windowsFiles.Where(windowsExpression.Compile()).ToList();
        filteredLinuxFiles.Should().HaveCount(1);
        filteredWindowsFiles.Should().HaveCount(1);
        filteredLinuxFiles[0].Id.Should().EndWith(filteredWindowsFiles[0].Id.ToString());
    }

    [Fact]
    public void ReplaceFunctionalBinaryExpressionParameterType5()
    {
        // Arrange
        var linuxFiles = LinuxFile.Faker.Generate(2); // LinuxFile is autogenerated based on WindowsFile
        var windowsFiles = linuxFiles.Adapt<List<WindowsFile>>(new TypeAdapterConfig());
        var prefix = "tenantAStageProd";
        linuxFiles.ForEach(x => x.Id = $"{prefix}{x.Id}");

        var firstFileId = windowsFiles[0].Id;

        Expression<Func<WindowsFile, bool>> windowsExpression = x => x.Id != firstFileId;

        // Act
        Expression<Func<LinuxFile, bool>> linuxExpression = windowsExpression
            .ReplaceFunctionalBinaryExpressionParameterType<WindowsFile, LinuxFile>(prefix);

        // Assert
        var filteredLinuxFiles = linuxFiles.Where(linuxExpression.Compile()).ToList();
        var filteredWindowsFiles = windowsFiles.Where(windowsExpression.Compile()).ToList();
        filteredLinuxFiles.Should().HaveCount(1);
        filteredWindowsFiles.Should().HaveCount(1);
        filteredLinuxFiles[0].Id.Should().EndWith(filteredWindowsFiles[0].Id.ToString());
    }

    [Fact]
    public void ReplaceFunctionalBinaryExpressionParameterType6()
    {
        // Arrange
        var linuxFiles = LinuxFile.Faker.Generate(2); // LinuxFile is autogenerated based on WindowsFile
        var windowsFiles = linuxFiles.Adapt<List<WindowsFile>>(new TypeAdapterConfig());
        var prefix = string.Empty;
        linuxFiles.ForEach(x => x.Id = $"{prefix}{x.Id}");

        var fileIds = new List<Guid>()
        {
            windowsFiles[0].Id,
            Guid.Empty,
            Guid.NewGuid(),
            Guid.NewGuid(),
        };

        Expression<Func<WindowsFile, bool>> windowsExpression = x => fileIds.Contains(x.Id);

        // Act
        Expression<Func<LinuxFile, bool>> linuxExpression = windowsExpression
            .ReplaceFunctionalBinaryExpressionParameterType<WindowsFile, LinuxFile>(prefix);

        // Assert
        var filteredLinuxFiles = linuxFiles.Where(linuxExpression.Compile()).ToList();
        var filteredWindowsFiles = windowsFiles.Where(windowsExpression.Compile()).ToList();
        filteredLinuxFiles.Should().HaveCount(1);
        filteredWindowsFiles.Should().HaveCount(1);
        filteredLinuxFiles[0].Id.Should().EndWith(filteredWindowsFiles[0].Id.ToString());
    }
}

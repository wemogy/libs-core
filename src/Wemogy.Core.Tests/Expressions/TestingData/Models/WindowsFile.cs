using System;
using Bogus;

namespace Wemogy.Core.Tests.Expressions.TestingData.Models;

public class WindowsFile
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public WindowsFile()
    {
        Name = string.Empty;
    }

    public static Faker<WindowsFile> Faker
    {
        get
        {
            return new Faker<WindowsFile>()
                .RuleFor(
                    x => x.Id,
                    f => f.Random.Guid())
                .RuleFor(
                    x => x.Name,
                    f => f.Name.FirstName());
        }
    }
}

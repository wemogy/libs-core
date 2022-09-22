using Bogus;

namespace Wemogy.Core.Tests.Expressions.TestingData.Models;

public class LinuxFile
{
    public string Id { get; set; }

    public string Name { get; set; }

    public LinuxFile()
    {
        Id = string.Empty;
        Name = string.Empty;
    }

    public static Faker<LinuxFile> Faker
    {
        get
        {
            return new Faker<LinuxFile>()
                .RuleFor(
                    x => x.Id,
                    f => f.Random.Guid().ToString())
                .RuleFor(
                    x => x.Name,
                    f => f.Name.FirstName());
        }
    }
}

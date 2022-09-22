namespace Wemogy.Core.Tests.Mapster.TestResources.Models;

public class User
{
    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public User()
    {
        Firstname = string.Empty;
        Lastname = string.Empty;
    }
}

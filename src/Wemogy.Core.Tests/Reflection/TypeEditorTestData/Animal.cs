namespace Wemogy.Core.Tests.Reflection.TypeEditorTestData;

public class Animal : EntityBase
{
    public string Name { get; set; }

    public Animal BestFriend { get; set; }

    [Dummy]
    public string ZooId { get; set; }

    public Animal()
    {
        Name = "Fluffy";
        BestFriend = new Animal();
        ZooId = "123";
    }
}

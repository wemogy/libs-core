namespace Wemogy.Core.Tests.Reflection.TypeEditorTestData;

public interface IEntity<TId>
{
    TId Id { get; }
}

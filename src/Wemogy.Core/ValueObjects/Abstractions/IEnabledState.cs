namespace Wemogy.Core.ValueObjects.Abstractions
{
    public interface IEnabledState
    {
        bool IsEnabled { get; }

        void Disable();

        void Enable();
    }
}

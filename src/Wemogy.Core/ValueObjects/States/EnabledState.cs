using Wemogy.Core.ValueObjects.Abstractions;

namespace Wemogy.Core.ValueObjects.States
{
    public class EnabledState : IEnabledState
    {
        public bool IsEnabled { get; private set; }

        public EnabledState(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }

        public void Disable()
        {
            IsEnabled = false;
        }

        public void Enable()
        {
            IsEnabled = true;
        }
    }
}

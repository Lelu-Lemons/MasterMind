namespace Mastermind.Components.Services
{
    public class AccessibilityService: IDisposable
    {
        public bool AccessibilityOn { get; set; } = false;
        public event Func<bool, Task>? AccessibilityNotify;
        public EventHandler? AccessibilityChanged;

        public async Task SetAccessibility(bool value)
        {
            AccessibilityOn = value;

            AccessibilityNotify?.Invoke(value);
            if (AccessibilityNotify != null)
            {
                AccessibilityChanged?.Invoke(this, EventArgs.Empty);
            }
            await Task.Delay(1);
        }

        public void Dispose() 
        {
            return;
        }
    }
}

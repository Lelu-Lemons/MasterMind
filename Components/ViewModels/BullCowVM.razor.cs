using Mastermind.Components.Services;
using Microsoft.AspNetCore.Components;

namespace Mastermind.Components.ViewModels
{
    public class BullCowVM : ComponentBase
    {
        [Inject]
        public required GameStateService gameState { get; set; }
        public bool BullInfoOpen { get; set; } = false;
        public bool CowInfoOpen { get; set; } = false;


		public async Task OnBullNotify(int value)
		{
			await InvokeAsync(() =>
			{
				StateHasChanged();
			});
		}

		public async Task OnCowNotify(int value)
		{
			await InvokeAsync(() =>
			{
				StateHasChanged();
			});
		}
		protected override async Task OnInitializedAsync()
		{
			gameState.bullNotify += OnBullNotify;
			gameState.cowNotify += OnCowNotify;
			await Task.Delay(1);
		}

		public void ToggleBullOpen()
        {
            BullInfoOpen = true;
        }

        public void ToggleBullClose()
        {
            BullInfoOpen = false;
        }

        public void ToggleCowOpen()
        {
            CowInfoOpen = true;
        }

        public void ToggleCowClose()
        {
            CowInfoOpen = false;
        }
    }
}

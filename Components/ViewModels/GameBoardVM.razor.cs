using Mastermind.Components.Services;
using Microsoft.AspNetCore.Components;

namespace Mastermind.Components.ViewModels
{
    public class GameBoardVM : ComponentBase
    {
        [Inject]
        public required GameStateService gameState { get; set; }

        public async Task SetChoice(int choice)
        {
            switch (gameState.firstAvailable)
            {
                case 1:
					await gameState.SetChoiceOne(choice);
					break;
                case 2:
                    await gameState.SetChoiceTwo(choice);
                    break;
                case 3:
                    await gameState.SetChoiceThree(choice);
                    break;
                case 4:
                    await gameState.SetChoiceFour(choice);  
                    break;
            }
            await gameState.FindFirstAvailable();
        }

        public async Task OnFirstAvailNotify(int value)
        {
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }



		protected override async Task OnInitializedAsync()
		{
            gameState.firstAvailableNotify += OnFirstAvailNotify;
            await Task.Delay(1);
		}
	}
}

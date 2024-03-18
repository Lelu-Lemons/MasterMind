using Mastermind.Components.Services;
using Microsoft.AspNetCore.Components;

namespace Mastermind.Components.ViewModels
{
	public class CurrentComboVM : ComponentBase
	{
		[Inject]
		public required GameStateService gameState { get; set; }
		[Parameter]
		public required Dictionary<int, string> colorDictionary { get; set; }
		[Parameter]
		public required IList<int> Combo { get; set; }

		public async Task ResetChoice(int choice)
		{
			switch (choice)
			{
				case 1:
					await gameState.SetChoiceOne(0);
					break;
				case 2:
					await gameState.SetChoiceTwo(0);
					break;
				case 3:
					await gameState.SetChoiceThree(0);
					break;
				case 4: 
					await gameState.SetChoiceFour(0);
					break;
			}
			gameState.FindFirstAvailable();
		}

	}
}

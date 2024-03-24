using Mastermind.Components.Services;
using Microsoft.AspNetCore.Components;

namespace Mastermind.Components.ViewModels
{
	public class CurrentComboVM : ComponentBase
	{
		[Inject]
		public required GameStateService gameState { get; set; }
		[Inject]
		public required LockSmithService lockSmith { get; set; }
		[Parameter]
		public required Dictionary<int, string> colorDictionary { get; set; }
		[Parameter]
		public required IList<int> Combo { get; set; }
		[Parameter]
		public bool AccessibilityState { get; set; }


		public async Task SubmitCombo() // CHECK BULL COWS AND SET ROUND SELECTION
		{
			await lockSmith.GetBullCows(Combo);
			await gameState.SetRoundChoice(Combo);
			await Task.Delay(1);
		}
		public async Task ResetChoice(int choice) // RESET INDIVIDUAL CHOICE SELECTION 
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
			await gameState.FindFirstAvailable(); 
		}

	}
}

using Mastermind.Components.Services;
using Microsoft.AspNetCore.Components;

namespace Mastermind.Components.Layout
{
	public class MainLayoutVM: LayoutComponentBase
	{
		[Inject]
		public required GameStateService gameState { get; set; }
		[Inject]
		public required LockSmithService lockSmith { get; set; }
		[Inject]
		public required AccessibilityService accessService { get; set; }
		public bool AccessibilityOn { get; set; } = false;
		public async Task ResetGame() 
		{
			await lockSmith.GenCode();
			await gameState.ResetBoard();
		}

		public async Task ChangeAccessibility()
		{
			AccessibilityOn = !AccessibilityOn;
			await accessService.SetAccessibility(AccessibilityOn);
		}
	}
}

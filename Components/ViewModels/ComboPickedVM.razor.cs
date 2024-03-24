using Microsoft.AspNetCore.Components;
using Mastermind.Components.Services;

namespace Mastermind.Components.ViewModels
{
	public class ComboPickedVM: ComponentBase
	{
		[Inject]
		public required GameStateService gameState { get; set; }
		[Parameter]
		public bool AccessibilityState { get; set; }
		[Parameter]
		public required Dictionary<int, string> colorDictionary { get; set; }
		[Parameter]
		public required IList<int> Combo { get; set; }
		[Parameter]
		public required int Attempt { get; set; }
		[Parameter]
		public required int Bulls { get; set; }
		[Parameter]
		public required int Cows { get; set; }


	}
}

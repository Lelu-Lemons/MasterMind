using Microsoft.AspNetCore.Components;
using Mastermind.Components.Constants;
using Mastermind.Components.Services;

namespace Mastermind.Components.ViewModels
{
	public class ComboPickedVM: ComponentBase
	{
		[Parameter]
		public required Dictionary<int, string> colorDictionary { get; set; }
		[Parameter]
		public required IList<int> Combo { get; set; }
		[Parameter]
		public required int Attempt { get; set; }




	}
}

using Mastermind.Components.Services;
using Microsoft.AspNetCore.Components;

namespace Mastermind.Components.ViewModels
{
	public class MainPageVM: ComponentBase
	{
		[Inject]
		public required LockSmithService lockSmith { get; set; }
		[Inject]
		public required GameStateService gameState { get; set; }



		public async Task OnPickListNotify(IList<int> list)
		{
			await InvokeAsync(() =>
			{
				StateHasChanged();
			});
		}

		public async Task OnChoiceNotify(Dictionary<int,int> choice)
		{
			await InvokeAsync(() =>
			{
				StateHasChanged();
			});
		}

		protected override async Task OnInitializedAsync()
		{
			lockSmith.NotifyPickedList += OnPickListNotify;
			gameState.choiceNotify += OnChoiceNotify;
			
			await Task.Delay(1);
		}

		protected override async void OnAfterRender(bool firstRender)
		{
			if (firstRender)
			{
				await lockSmith.GenCode();
				await gameState.GenChoiceSelection();
			}
		}

		public void Dispose()
		{
			lockSmith.NotifyPickedList -= OnPickListNotify;
		}
	}
}

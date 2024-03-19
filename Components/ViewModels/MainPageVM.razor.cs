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

		public async Task OnRoundNotify(Dictionary<int,Dictionary<int,int>> round)
		{
			var currentBull = gameState.BullState;
			var currentCow = gameState.CowState;
			await gameState.SetBullCow(currentBull, currentCow);
			await InvokeAsync(() =>
			{
				StateHasChanged();
			});
		}

		public async Task OnBullNotify(int value)
		{
			await gameState.GetBulls(value);
			await InvokeAsync(() =>
			{
				StateHasChanged();
			});
		}

		public async Task OnCowNotify(int value)
		{
			await gameState.GetCows(value);
			await InvokeAsync(() =>
			{
				StateHasChanged();
			});
		}

		public async Task OnRoundBullCowNotify(Dictionary<int, Dictionary<string, int>> roundBullCow)
		{
			await InvokeAsync(() =>
			{
				StateHasChanged();
			});
		}

		protected override async Task OnInitializedAsync()
		{
			lockSmith.NotifyPickedList += OnPickListNotify;
			lockSmith.BullNotify += OnBullNotify;
			lockSmith.CowNotify += OnCowNotify;

			gameState.choiceNotify += OnChoiceNotify;
			gameState.roundNotify += OnRoundNotify;
			gameState.roundBullCowNotify += OnRoundBullCowNotify;
			await Task.Delay(1);
		}

		protected override async void OnAfterRender(bool firstRender)
		{
			if (firstRender)
			{
				await lockSmith.GenCode();
				await gameState.GenChoiceSelection();
				await gameState.GenRoundChoice();
				await gameState.GenRoundBullCows();
			}
		}

		public void Dispose()
		{
			lockSmith.NotifyPickedList -= OnPickListNotify;
			lockSmith.BullNotify -= OnBullNotify;
			lockSmith.CowNotify -= OnCowNotify;


			gameState.choiceNotify -= OnChoiceNotify;
			gameState.roundNotify -= OnRoundNotify;
			gameState.roundBullCowNotify -= OnRoundBullCowNotify;
		}
	}
}

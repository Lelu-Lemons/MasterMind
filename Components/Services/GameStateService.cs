namespace Mastermind.Components.Services
{
    public class GameStateService : IDisposable
    {
		public readonly Dictionary<int, string> colorDictionary = new Dictionary<int, string>
		{
			{ 0, "background-color: rgb(77, 77, 77); color:rgb(140, 140, 140);" },
			{ 1, "background-color: darkcyan; color: aqua;" },
			{ 2, "background-color: rgb(154, 156, 33); color: rgb(233, 235, 73);" },
			{ 3, "background-color: rgb(140, 52, 55); color: rgb(250, 120, 125);" },
			{ 4, "background-color: rgb(108, 41, 135); color:rgb(213, 120, 250);" },
			{ 5, "background-color: rgb(156, 54, 141); color:rgb(247, 116, 228);" },
			{ 6, "background-color: rgb(17, 2, 158); color: rgb(59, 38, 255);" },
			{ 7, "background-color: rgb(42, 130, 54); color: rgb(100, 245, 120);" },
			{ 8, "background-color: rgb(163, 109, 8); color: rgb(255, 179, 36);" },
		};

		public Dictionary<int, int> choiceSelection { get; set; } = new Dictionary<int, int>();
		public Dictionary<int, Dictionary<int, int>> roundPicks { get; set; } = new Dictionary<int, Dictionary<int, int>>();
        public int round { get; set; } = 1;
        public int firstAvailable { get; set; } = 1;

        public event Func<Dictionary<int, int>, Task>? choiceNotify;
        public event Func<Dictionary<int,Dictionary<int,int>>, Task>? roundNotify;
        public event Func<int, Task>? firstAvailableNotify;

        public EventHandler? choiceChanged;
        public EventHandler? roundChanged;
        public EventHandler? firstAvailableChanged;

        public void FindFirstAvailable()
        {
            for(int i = 1; i <= choiceSelection.Count; i++)
            {
                if (choiceSelection[i] == 0)
                {
                    firstAvailable = i;
                    return;
                }
            }
        }

        public async Task SetRoundChoice(Dictionary<int, int> roundChoice)
        {
            roundPicks[this.round] = roundChoice;

            roundNotify?.Invoke(roundPicks);
            if (roundNotify != null)
            {
                roundChanged?.Invoke(this, EventArgs.Empty);
            }
            await Task.Delay(1);
        }
        public async Task GenChoiceSelection()
        {
            choiceSelection.Clear();
            choiceSelection[1] = 0;
            choiceSelection[2] = 0;
            choiceSelection[3] = 0;
            choiceSelection[4] = 0;

            choiceNotify?.Invoke(choiceSelection);
            if (choiceNotify != null)
            {
                choiceChanged?.Invoke(this, EventArgs.Empty);
            }
            await Task.Delay(1);
        }
		public async Task SetChoiceOne(int choice)
		{
			choiceSelection[1] = choice;
            choiceNotify?.Invoke(choiceSelection);
            if (choiceNotify != null)
            {
                choiceChanged?.Invoke(this, EventArgs.Empty);
            }
            await Task.Delay(1);
		}
        public async Task SetChoiceTwo(int choice)
        {
            choiceSelection[2] = choice;
            choiceNotify?.Invoke(choiceSelection);
            if (choiceNotify != null)
            {
                choiceChanged?.Invoke(this, EventArgs.Empty);
            }
            await Task.Delay(1);
        }
        public async Task SetChoiceThree(int choice)
        {
            choiceSelection[3] = choice;
            choiceNotify?.Invoke(choiceSelection);
            if (choiceNotify != null)
            {
                choiceChanged?.Invoke(this, EventArgs.Empty);
            }
            await Task.Delay(1);
        }
        public async Task SetChoiceFour(int choice)
        {
            choiceSelection[4] = choice;
            choiceNotify?.Invoke(choiceSelection);
            if (choiceNotify != null)
            {
                choiceChanged?.Invoke(this, EventArgs.Empty);
            }
            await Task.Delay(1);
        }
        public void Dispose() 
        {
            return;
        }
    }
}

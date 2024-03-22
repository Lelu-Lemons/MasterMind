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
        public Dictionary<int, int> tempChoice { get; set; } = new Dictionary<int, int>();
        public Dictionary<int, Dictionary<int, int>> roundPicks { get; set; } = new Dictionary<int, Dictionary<int, int>>();
        public Dictionary<int, Dictionary<string, int>> roundBullCows { get; set; } = new Dictionary<int, Dictionary<string, int>>();
        public int round { get; set; } = 1;
        public bool gameOver { get; set; } = false;
        public bool winState { get; set; } = false;
        public int firstAvailable { get; set; } = 1;
        public int BullState { get; set; }
        public int CowState { get; set; }
        public Dictionary<string, int> BullCowSingleRound { get; set; } = new Dictionary<string, int>();

        public event Func<Dictionary<int, int>, Task>? choiceNotify;
        public event Func<Dictionary<int,Dictionary<int,int>>, Task>? roundNotify;
        public event Func<Dictionary<int, Dictionary<string, int>>, Task>? roundBullCowNotify;
        public event Func<int, Task>? firstAvailableNotify;
        public event Func<bool, Task>? gameOverNotify;
        public event Func<bool, Task>? winStateNotify;
        public event Func<int, Task>? bullNotify;
        public event Func<int, Task>? cowNotify;

        public EventHandler? choiceChanged;
        public EventHandler? roundChanged;
        public EventHandler? roundbullCowChanged;
        public EventHandler? firstAvailableChanged;
        public EventHandler? gameOverChanged;
        public EventHandler? winStateChanged;
        public event EventHandler? bullStateChanged;
        public event EventHandler? cowStateChanged;

        public async Task GetBulls(int value)
        {
            BullState = value;
            BullCowSingleRound["Bulls"] = BullState;
            bullNotify?.Invoke(BullState);
            if (bullNotify != null)
            {
                bullStateChanged?.Invoke(this, EventArgs.Empty);
            }
            await Task.Delay(1);
        }

        public async Task GetCows(int value)
        {
            CowState = value;
            BullCowSingleRound["Cows"] = CowState;
            cowNotify?.Invoke(CowState);
            if (cowNotify != null)
            {
                cowStateChanged?.Invoke(this, EventArgs.Empty);
            }

            await Task.Delay(1);
        }

        public async Task FindFirstAvailable()
        {
            for(int i = 1; i <= choiceSelection.Count; i++)
            {
                if (choiceSelection[i] == 0)
                {
                    firstAvailable = i;
                    break;
                }
            }
            await Task.Delay(1);
        }

        public async Task ResetBoard()
        {
            round = 1;
            BullCowSingleRound.Clear();
            await GetBulls(0);
            await GetCows(0);
            gameOver = false;
            winState = false;
            await GenChoiceSelection();
            await GenRoundChoice();
            await GenRoundBullCows();

            gameOverNotify?.Invoke(gameOver);
            if (gameOverNotify != null)
            {
                gameOverChanged?.Invoke(this, EventArgs.Empty);
            }
            winStateNotify?.Invoke(winState);
            if (winStateNotify != null)
            {
                winStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public async Task SetBullCow()
        {
            Console.WriteLine("set bull cow");
            var tempRound = round;

            var tempDict = new Dictionary<string, int>();
            tempDict["Bulls"] = BullCowSingleRound["Bulls"];
            tempDict["Cows"] = BullCowSingleRound["Cows"];
            roundBullCows[tempRound] = tempDict;

            roundBullCowNotify?.Invoke(roundBullCows);
            if (roundBullCowNotify != null)
            {
                roundbullCowChanged?.Invoke(this, EventArgs.Empty);
            }

            if (BullCowSingleRound["Bulls"] == 4 || tempRound == 7)
            {
                gameOver = true;
                if (BullCowSingleRound["Bulls"] == 4)
                {
                    winState = true;
                    winStateNotify?.Invoke(winState);
                    if (winStateNotify != null)
                    {
                        winStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                }
			}
			gameOverNotify?.Invoke(gameOver);
			if (gameOverNotify != null)
			{
				gameOverChanged?.Invoke(this, EventArgs.Empty);
			}
			await Task.Delay(1);
        }

        public async Task GenRoundBullCows()
        {
            roundBullCows.Clear();
            var tempBullCow = new Dictionary<string, int>();
            tempBullCow["Bulls"] = 0;
            tempBullCow["Cows"] = 0;
            roundBullCows[1] = tempBullCow;
            roundBullCows[2] = tempBullCow;
            roundBullCows[3] = tempBullCow;
            roundBullCows[4] = tempBullCow;
            roundBullCows[5] = tempBullCow;
            roundBullCows[6] = tempBullCow;
            roundBullCows[7] = tempBullCow;

            roundBullCowNotify?.Invoke(roundBullCows);
            if (roundBullCowNotify != null)
            {
                roundbullCowChanged?.Invoke(this, EventArgs.Empty);
            }

            await Task.Delay(1);
        }

        public async Task GenRoundChoice()
        {
            Console.WriteLine("round gen");
            tempChoice[1] = 0;
            tempChoice[2] = 0;
            tempChoice[3] = 0;
            tempChoice[4] = 0;
            roundPicks[1] = tempChoice;
            roundPicks[2] = tempChoice;
            roundPicks[3] = tempChoice;
            roundPicks[4] = tempChoice;
            roundPicks[5] = tempChoice;
            roundPicks[6] = tempChoice;
            roundPicks[7] = tempChoice;

			roundNotify?.Invoke(roundPicks);
			if (roundNotify != null)
			{
				roundChanged?.Invoke(this, EventArgs.Empty);
			}
			await Task.Delay(1);
		}

        public async Task SetRoundChoice(IList<int> Combo)
        {
            var tempRound = round;
            var tempCombo = new Dictionary<int, int>();
            tempCombo[0] = Combo[0];
            tempCombo[1] = Combo[1];
            tempCombo[2] = Combo[2];
            tempCombo[3] = Combo[3];
            roundPicks[tempRound] = tempCombo;

            roundNotify?.Invoke(roundPicks);
            if (roundNotify != null)
            {
                roundChanged?.Invoke(this, EventArgs.Empty);
            }
            round += 1;
            await Task.Delay(1);
			await GenChoiceSelection();
		}

        public async Task QuickChoiceGen()
        {
			choiceSelection.Clear();
			choiceSelection[1] = 0;
			choiceSelection[2] = 0;
			choiceSelection[3] = 0;
			choiceSelection[4] = 0;
            await Task.Delay(1);
		}
        public async Task GenChoiceSelection()
        {
            await QuickChoiceGen();

            choiceNotify?.Invoke(choiceSelection);
            if (choiceNotify != null)
            {
                choiceChanged?.Invoke(this, EventArgs.Empty);
            }
            await FindFirstAvailable();
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

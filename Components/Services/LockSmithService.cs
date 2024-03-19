namespace Mastermind.Components.Services
{
    public class LockSmithService: IDisposable
    {
        private IList<int> pickedList { get; set; } = new List<int>();
        // Gen Code
        public int CurrentBulls { get; set; } = 0;
        public int CurrentCows { get; set; } = 0;
        Random? random { get; set; }

        public event Func<IList<int>, Task>? NotifyPickedList;
        public EventHandler? PickListChanged;
        public async Task GenCode()
        {
            pickedList.Clear();
            random = new Random();
            while (pickedList.Count < 4)
            {
                var num = random.Next(1,9);
                if (!pickedList.Contains(num))
                {
                    pickedList.Add(num);
                }
            }

            foreach (var num in pickedList)
            {
                Console.WriteLine(num);
            }
            await Task.Delay(1);
        }

        public async Task GetBullCows(IList<int> comboToCheck)
        {
            
            for (int i = 0; i < pickedList.Count; i++)
            {
                var pick = pickedList[i];
                var comboPick = comboToCheck[i];
                if (pick == comboPick)
                {
                    CurrentBulls++;
                }
                else if (pickedList.Contains(comboPick)) 
                {
                    CurrentCows++;   
                }

            }
            await Task.Delay(1);
        }

        public void Dispose()
        {
            pickedList.Clear();
            Console.WriteLine("failed");
            return;
        }

        // Check Code, returns the bulls cow object
    }
}

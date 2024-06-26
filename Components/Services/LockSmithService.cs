﻿using System.Collections;
using System.Diagnostics;

namespace Mastermind.Components.Services
{
    public class LockSmithService: IDisposable
    {
        private IList<int> pickedList { get; set; } = new List<int>();
        // Gen Code
        public int CurrentBulls { get; set; } = 0;
        public int CurrentCows { get; set; } = 0;
        Random? random { get; set; }

        public event Func<int, Task>? BullNotify;
        public event Func<int, Task>? CowNotify;

        public EventHandler? BullChanged;
        public EventHandler? CowChanged;


        public async Task GenCode() // GENERATE 4 DIGIT SECRET CODE ( 1-8 )
        {   // DOES NOT LEAVE SERVICE
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

            foreach (var num in pickedList) // DEBUG CHECK CODE
            {
                Debug.WriteLine(num);
            }
            await Task.Delay(1);
        }

        public async Task GetBullCows(IList<int> comboToCheck) // CHECK COMBO GUESSED AND RETURN BULLS AND COWS
        {   // CHECK SET NOTIFY
            CurrentBulls = 0;
            CurrentCows = 0;

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
            

            BullNotify?.Invoke(CurrentBulls);
            if (BullNotify != null)
            {
                BullChanged?.Invoke(this, EventArgs.Empty);
            }

            CowNotify?.Invoke(CurrentCows);
            if (CowNotify != null)
            {
                CowChanged?.Invoke(this, EventArgs.Empty);
            }

            await Task.Delay(1);
        }

        public void Dispose()
        {
            pickedList.Clear();
            return;
        }

    }
}

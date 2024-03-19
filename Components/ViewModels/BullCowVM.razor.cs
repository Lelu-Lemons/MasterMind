using Mastermind.Components.Services;
using Microsoft.AspNetCore.Components;

namespace Mastermind.Components.ViewModels
{
    public class BullCowVM : ComponentBase
    {
        public bool BullInfoOpen { get; set; } = false;
        public bool CowInfoOpen { get; set; } = false;
       
        
        public void ToggleBullOpen()
        {
            BullInfoOpen = true;
        }

        public void ToggleBullClose()
        {
            BullInfoOpen = false;
        }

        public void ToggleCowOpen()
        {
            CowInfoOpen = true;
        }

        public void ToggleCowClose()
        {
            CowInfoOpen = false;
        }
    }
}

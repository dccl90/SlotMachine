using System.Collections.Immutable;
using System.Reflection.PortableExecutable;
namespace SlotMachine
{
    internal class Program
    {
        
        static void Main(string[] args)
        { 
            
            UserInterface.InputMoney();
            UserInterface.SelectPlayingMode();  

            while(true)
            {
                UserInterface.InputBet();
                UserInterface.PrintNumbers();
                bool win = SlotMachineLogic.CheckWin();
                SlotMachineLogic.MulitplyMoney(win);
                
                if(win)
                {
                    UserInterface.PrintWinnerMessage();
                }   

                bool isMoneyAvailable = SlotMachineLogic.IsMoneyAvailable();
                if(!isMoneyAvailable)
                {
                    UserInterface.PrintNoMoneyMessage();
                    break;
                }

                char betAgain = UserInterface.InputContinueGame();
                if(betAgain.Equals(Constants.EXIT_GAME))
                {
                    break;
                }
            }
        }
    }
}
using System.Collections.Immutable;
using System.Reflection.PortableExecutable;
namespace SlotMachine
{
    internal class Program
    {
        
        static void Main(string[] args)
        { 
            
            double inputMoney = UserInterface.InputMoney();
            char playingMode = UserInterface.SelectPlayingMode();  

            while(true)
            {
                UserInterface.InputBet();
                UserInterface.PrintNumbers();
                bool win = SlotMachineLogic.CheckWin();
                bool jackpot = SlotMachineLogic.GetJackpot();
                bool minorJackpot = SlotMachineLogic.GetMinorJackpot();
                SlotMachineLogic.MulitplyMoney(win);
                if(jackpot)
                {
                    UserInterface.PrintJackpotMessage();
                }

                if(minorJackpot && !jackpot)
                {
                    UserInterface.PrintMinorJackpotMessage();
                }

                if(win && !jackpot && !minorJackpot)
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
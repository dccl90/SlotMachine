using System.Collections.Immutable;
namespace SlotMachine
{
    internal class Program
    {

            const int ROWS = 3;
            const int COLUMNS = 3;
            const int DIAGONAL_LINES = 2;
            const int RANGE_START = 1;
            const int RANGE_END = 3;
            const double MIN_BET = 1;
            const double WIN_MULTIPLIER = 2;
            const double JACKPOT_MULTIPLIER = 10;
            const double MINOR_JACKPOT_MULTIPLIER = 5;
            const char EXIT_GAME = 'n';
            const char ALL_ROWS = 'R';
            const char ALL_COLUMNS = 'C';
            const char ALL_DIAGONALS = 'D';
            const char ALL_LINES = 'E';
        
        static void Main(string[] args)
        { 

    
            
            double inputMoney = UserInterface.InputMoney();
            char playingMode = UserInterface.SelectPlayingMode();
            
            

            while(true)
            {
                double bet = UserInterface.InputBet(inputMoney, playingMode);
                inputMoney = SlotMachineLogic.SubtractBetFromAvailableMoney(bet, inputMoney);
                SlotMachineLogic.GenerateNumbers();
                int[,] numbers = SlotMachineLogic.GetNumbers();
                UserInterface.PrintNumbers(numbers);
                SlotMachineLogic.CheckWin(inputMoney, bet, playingMode);
                

                //If money is less then 1, end the game.
                if(inputMoney < MIN_BET)
                {
                    Console.WriteLine("\t No more Money Available");
                    break;
                }

                //Prompt user to place another bet
                Console.Write("\t Press N to exit: ");
                char betAgain = Char.ToLower(Console.ReadKey().KeyChar);
                if(betAgain.Equals(EXIT_GAME))
                {
                    break;
                }
                Console.Clear();
            }
        }
    }
}
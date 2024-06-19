using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace SlotMachine
{
    internal class UserInterface
    {

        /// <summary>
        /// Asks the user to input a value and checks if it is a valid
        /// </summary>
        /// <returns>Returns the money input by the user</returns>
        public static void InputMoney()
        {
            bool isInputValid;
            do
            {
                ClearConsole();
                Console.Write("\t Add Money : $");
                string inputMoney = Console.ReadLine();
                isInputValid = SlotMachineLogic.ValidateInput(inputMoney);
                if(!isInputValid)
                {
                    PrintInvalidInputMessage();
                    continue;
                }

                double availableMoney = Double.Parse(inputMoney);
                SlotMachineLogic.SetAvailableMoney(availableMoney);
            } 
            while (!isInputValid);
        }

        /// <summary>
        /// Prints a message indicating the number input was invalid
        /// </summary>
        private static void PrintInvalidInputMessage()
        {
            ClearConsole();
            Console.WriteLine("\t Please enter a valid number");
            PrintPressAnyKeyToContinue();
        }

        /// <summary>
        /// Asks the user to input the playing mode and checks if it is a valid
        /// </summary>
        /// <returns>Returns the playing mode as a char</returns>
        public static void SelectPlayingMode()
        {
            bool isPlayingModeValid;
            do
            {

                PrintPlayingOptions();
                Console.Write("\t Enter Mode: ");
                char playingMode = Char.ToUpper(Console.ReadKey().KeyChar);
                isPlayingModeValid = SlotMachineLogic.ValidatePlayingMode(playingMode);
                if (!isPlayingModeValid)
                {
                    PrintInvalidPlayingModeMessage();
                    continue;
                }
                SlotMachineLogic.SetPlayingMode(playingMode);
                break;
            } 
            while (!isPlayingModeValid);

        }

        /// <summary>
        /// A method for entering the bet ammound
        /// </summary>
        public static void InputBet()
        {
            SlotMachineLogic.GetAvailableMoney();
            bool isInputBetValid = false;
            do
            {
                PrintGameHeader();
                Console.Write($"\t Enter Bet Amount (Min Bet ${Constants.MIN_BET}): $");
                string inputBet = Console.ReadLine();
                isInputBetValid = SlotMachineLogic.ValidateInputBet(inputBet);
                if(isInputBetValid)
                {
                    double bet = Double.Parse(inputBet);
                    SlotMachineLogic.SetBet(bet);
                    SlotMachineLogic.SubtractBetFromAvailableMoney();
                    PrintGameHeader();
                    Console.Write($"\t Enter Bet Amount (Min Bet ${Constants.MIN_BET}): ${bet}");
                    Console.WriteLine();
                }
                if(!isInputBetValid)
                {
                    PrintInvalidBetMessage();
                    continue;
                }
            }
            while(!isInputBetValid);
        }

        /// <summary>
        /// A method to collect the playing mode from the player.
        /// </summary>
        /// <return>The playing mode represented as a char</return>
        public static char InputContinueGame()
        {
            Console.Write("\t Press N to exit: ");
            return Char.ToLower(Console.ReadKey().KeyChar);
        }

        /// <summary>
        /// Prints the numbers array to the console
        /// </summary>
        public static void PrintNumbers()
        {
            SlotMachineLogic.GenerateNumbers();
            int[,] numbers = SlotMachineLogic.GetNumbers();
            for(int i = 0; i < numbers.GetLength(0); i++)
            {
                for(int j = 0; j < numbers.GetLength(1); j++)
                {  
                    Console.Write($"\t {numbers[i,j]} \t");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// A method for printing the winner message
        /// </summary>
        public static void PrintWinnerMessage(){
            double bet = SlotMachineLogic.GetBet();
            Console.WriteLine($"\t WINNER!!! You won {bet * Constants.WIN_MULTIPLIER}");
        }

        /// <summary>
        /// A method for printing the minor jackpot message
        /// </summary>
        public static void PrintMinorJackpotMessage()
        {
            double bet = SlotMachineLogic.GetBet();
            Console.WriteLine($"\t MINOR JACKPOT!!! You won {bet * Constants.MINOR_JACKPOT_MULTIPLIER}");
        }


        /// <summary>
        /// A method for printing the jackpot message
        /// </summary>
        public static void PrintJackpotMessage()
        {
            double bet = SlotMachineLogic.GetBet();
            Console.WriteLine($"\t JACKPOT!!! You won {bet * Constants.JACKPOT_MULTIPLIER}");
        }

        /// <summary>
        /// A method for printing the no available money message
        /// </summary>
        public static void PrintNoMoneyMessage()
        {
            Console.WriteLine("\t No more Money Available");
        }

        /// <summary>
        /// A method for printing the list of playing modes
        /// </summary>
        private static void PrintPlayingOptions()
        {
            ClearConsole();
            Console.WriteLine("\t Please enter your playing mode.");
            Console.WriteLine($"\t Enter {Constants.ALL_ROWS} to play all rows");
            Console.WriteLine($"\t Enter {Constants.ALL_COLUMNS} to play all columns");
            Console.WriteLine($"\t Enter {Constants.ALL_DIAGONALS} to play all diagonals");
            Console.WriteLine($"\t Enter {Constants.ALL_LINES} to play everything");
        }

        /// <summary>
        /// A method for printing the game header disaplying the playing mode and available money
        /// </summary>
        private static void PrintGameHeader()
        {
            char playingMode = SlotMachineLogic.GetPlayingMode();
            double availableMoney = SlotMachineLogic.GetAvailableMoney();
            ClearConsole();
            Console.WriteLine("\t #####Lucky Dynasty Slots#####");
            Console.WriteLine($"\t Available Money: ${availableMoney}");
            Console.WriteLine($"\t Game Mode: {playingMode}");
        }

        /// <summary>
        /// Prints a message indicating the playing mode is invalid
        /// </summary>
        private static void PrintInvalidPlayingModeMessage()
        {
            ClearConsole();
            Console.WriteLine($"\t Please enter {Constants.ALL_ROWS}, {Constants.ALL_COLUMNS}, {Constants.ALL_DIAGONALS} or {Constants.ALL_LINES}");
            PrintPressAnyKeyToContinue();
        }

        /// <summary>
        /// A method for printing an invalid bet message
        /// </summary>
        private static void PrintInvalidBetMessage()
        {
            Console.WriteLine("\t Bet must be a number greater then $1.00");
            PrintPressAnyKeyToContinue();
        }

        /// <summary>
        /// Prints a message asking the user to press any key to continue
        /// </summary>
        private static void PrintPressAnyKeyToContinue()
        {
            Console.WriteLine("\t Press any key to continue");
            Console.ReadLine();
        }

        /// <summary>
        /// Clears the console
        /// </summary>
        private static void ClearConsole()
        {
            Console.Clear();
        }
    }
}
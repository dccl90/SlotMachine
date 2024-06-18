using System.Reflection.PortableExecutable;

namespace SlotMachine
{
    internal class UserInterface
    {

        /// <summary>
        /// Asks the user to input a value and checks if it is a valid
        /// </summary>
        /// <returns>Returns the money input by the user</returns>
        public static double InputMoney()
        {
            bool isInputValid = false;
            string inputMoney;
            do
            {
                ClearConsole();
                Console.Write("\t Add Money : $");
                inputMoney = Console.ReadLine();
                isInputValid = SlotMachineLogic.ValidateInput(inputMoney);
                if(!isInputValid)
                {
                    PrintInvalidInputMessage();
                    continue;
                }
            } 
            while (!isInputValid);
            return Double.Parse(inputMoney);
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
        public static Char SelectPlayingMode()
        {
            char playingMode;
            while(true)
            {
                ClearConsole();
                Console.WriteLine("\t Please enter your playing mode.");
                Console.WriteLine($"\t Enter {Constants.ALL_ROWS} to play all rows");
                Console.WriteLine($"\t Enter {Constants.ALL_COLUMNS} to play all columns");
                Console.WriteLine($"\t Enter {Constants.ALL_DIAGONALS} to play all diagonals");
                Console.WriteLine($"\t Enter {Constants.ALL_LINES} to play everything");
                Console.Write("\t Enter Mode: ");
                playingMode = Char.ToUpper(Console.ReadKey().KeyChar);
                bool isPlayingModeValid = SlotMachineLogic.ValidatePlayingMode(playingMode);
                if(!isPlayingModeValid)
                {
                    PrintInvalidPlayingModeMessage();
                    continue;
                }
                break;
            }
            return playingMode;
        }

        public static double InputBet(double inputMoney, char playingMode){
            string inputBet;
            double bet = 0;
            bool isInputBetValid = false;
            do
            {
                ClearConsole();
                Console.WriteLine("\t #####Lucky Dynasty Slots#####");
                Console.WriteLine($"\t Available Money: ${inputMoney}");
                Console.WriteLine($"\t Game Mode: {playingMode}");
                Console.Write($"\t Enter Bet Amount (Min Bet ${Constants.MIN_BET}): $");
                inputBet = Console.ReadLine();

                isInputBetValid = SlotMachineLogic.ValidateInputBet(inputBet, inputMoney);

                if(isInputBetValid)
                {
                    bet = Double.Parse(inputBet);
                    inputMoney = SlotMachineLogic.SubtractBetFromAvailableMoney(bet, inputMoney);
                    ClearConsole();
                    Console.WriteLine("\t #####Lucky Dynasty Slots#####");
                    Console.WriteLine($"\t Available Money: ${inputMoney}");
                    Console.WriteLine($"\t Game Mode: {playingMode}");
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

            return bet;
        }

        public static void PrintNumbers(int[,] numbers)
        {
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
        /// Prints a message indicating the playing mode is invalid
        /// </summary>
        private static void PrintInvalidPlayingModeMessage()
        {
            ClearConsole();
            Console.WriteLine($"\t Please enter {Constants.ALL_ROWS}, {Constants.ALL_COLUMNS}, {Constants.ALL_DIAGONALS} or {Constants.ALL_LINES}");
            PrintPressAnyKeyToContinue();
        }

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
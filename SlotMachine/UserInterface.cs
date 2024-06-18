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
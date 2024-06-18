namespace SlotMachine
{
    internal class SlotMachineLogic
    {
        
        static Random rnd = new Random();
        static int[,] numbers = new int[Constants.ROWS,Constants.COLUMNS];


        public static void GenerateNumbers()
        {
             
            for(int i = 0; i < numbers.GetLength(0); i++)
            {
                for(int j = 0; j < numbers.GetLength(1); j++)
                {  
                    numbers[i,j] = rnd.Next(Constants.RANGE_START,Constants.RANGE_END);
                }
            }
        }

        public static int[,] GetNumbers()
        {
            return numbers;
        }
        /// <summary>
        /// Checks if the input is valid
        /// </summary>
        /// <param name="input">The money input by the player</param>
        /// <returns>Returns a bool that determines if the input is valid</returns>
        public static bool ValidateInput(string input)
        {
            bool isDouble = Double.TryParse(input, out double money);

            if(isDouble && money > Constants.MIN_BET){
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the playing mode is valid
        /// </summary>
        /// <param name="mode">The playing mode input by the player</param>
        /// <returns>Returns a bool that determines if the mode is valid</returns>
        public static bool ValidatePlayingMode(char mode )
        {
            if(
                !mode.Equals(Constants.ALL_ROWS) && 
                !mode.Equals(Constants.ALL_COLUMNS) && 
                !mode.Equals(Constants.ALL_DIAGONALS) && 
                !mode.Equals(Constants.ALL_LINES)
            )
            {
                return false;
            }
                return true; 
        }

        /// <summary>
        /// Checks if the bet input is valid
        /// </summary>
        /// <param name="inputBet">The bet input by the player</param>
        /// <returns>Returns a bool that determines if the bet is valid</returns>
        public static bool ValidateInputBet(string inputBet, double availableMoney)
        {
            bool isBetValid = Double.TryParse(inputBet, out double bet);
            if(isBetValid && bet >= Constants.MIN_BET && bet <= availableMoney)
            {
                return true;
            }

            return false;
        }

        public static Double SubtractBetFromAvailableMoney(double bet, double availableMoney)
        {
            return availableMoney - bet;
        }

    }
}
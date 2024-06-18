namespace SlotMachine
{
    internal class SlotMachineLogic
    {
        
        /// <summary>
        /// Checks if the input is valid
        /// </summary>
        /// <param name="input">The money input by the player</param>
        /// <returns>Returns a bool that determines if the input was valid</returns>
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
        /// <returns>Returns a bool that determines if the mode was valid</returns>
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

    }
}
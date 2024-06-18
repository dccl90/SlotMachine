namespace SlotMachine
{
    internal class SlotMachineLogic
    {
        
        /// <summary>
        /// Checks if the position input is availble
        /// </summary>
        /// <param name="input">The money input by the </param>
        /// <returns>If the position input is available</returns>
        public static bool ValidateInput(string input){
            bool isDouble = Double.TryParse(input, out double money);

            if(isDouble && money > Constatns.MIN_BET){
                return true;
            }
            return false;
        }
        
    }
}
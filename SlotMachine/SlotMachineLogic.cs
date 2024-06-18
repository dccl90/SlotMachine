namespace SlotMachine
{
    internal class SlotMachineLogic
    {
        
        static Random rnd = new Random();
        static int[,] numbers = new int[Constants.ROWS,Constants.COLUMNS];


        /// <summary>
        /// Generates the random numbers for the slot machine
        /// </summary>
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

        /// <summary>
        /// Getter method to return the numbers array
        /// </summary>
        /// <returns>Returns the array of numbers to be printed on the slot machine</returns>
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

        /// <summary>
        /// A method to subtract the bet ammount from the avaiable money
        /// </summary>
        /// <returns>Returns the available money minus the bet ammount</returns>
        public static Double SubtractBetFromAvailableMoney(double bet, double availableMoney)
        {
            return availableMoney -= bet;
        }

        public static void CheckWin(double inputMoney, double bet, char playingMode) 
        {
            bool win = false;  
                bool jackpot = false;
                bool minorJackpot = false;
                int columnWinCheck;
                int rowWinCheck;
                int columnWinCount = 0;
                int rowWinCount = 0;
                int diagonalWinCount = 0;

                //Loop over Rows
                if(playingMode.Equals(Constants.ALL_ROWS) || playingMode.Equals(Constants.ALL_LINES))
                {
                    for(int row = 0; row < Constants.ROWS; row++)
                    {   
                        rowWinCheck = 0; 
                        for(int column = 0; column < Constants.COLUMNS; column++)
                        {  
                            if(numbers[row,0] == numbers[row,column])
                            {
                                rowWinCheck++;
                            }
                        }

                        if(rowWinCheck == Constants.ROWS)
                        {
                            rowWinCount++;
                            win = true;
                        }

                        if(rowWinCount == Constants.ROWS)
                        {
                            minorJackpot = true;
                        }   
                    }
                }
                
                //Loop over Columns
                if(playingMode.Equals(Constants.ALL_COLUMNS) || playingMode.Equals(Constants.ALL_LINES))
                {
                    for(int row = 0; row < Constants.ROWS; row++)
                    {   
                        columnWinCheck = 0;
                        for(int column = 0; column < Constants.COLUMNS; column++)
                        {
                            if(numbers[0, row] == numbers[column,row])
                            {
                                columnWinCheck++;       
                            }
                        }

                        if(columnWinCheck == Constants.COLUMNS)
                        {
                            columnWinCount++;
                            win = true;
                        }

                        if(columnWinCount == Constants.COLUMNS)
                        {
                            minorJackpot = true;
                        }   
                    }
                }

                //Loop over diagonal lines 
                if(playingMode.Equals(Constants.ALL_DIAGONALS) || playingMode.Equals(Constants.ALL_LINES)){
                    int diagonalWinCheck = 0;
                    for(int row = 0; row < Constants.ROWS; row++)
                    {
                        
                        if(numbers[0, 0] == numbers[row,row]) 
                        {
                            diagonalWinCheck++;
                        }
                            
                        if(diagonalWinCheck == Constants.ROWS)
                        {
                            diagonalWinCount++;
                            win = true;
                        }              
                    }

                    diagonalWinCheck = 0;
                    for(int row = 0; row < Constants.ROWS; row++)
                    {
                        //Check diagonal win
                        if(numbers[0, Constants.COLUMNS - 1] == numbers[row,Constants.COLUMNS - 1 - row]) 
                        {
                            diagonalWinCheck++;
                        }
                            
                        if(diagonalWinCheck == Constants.ROWS)
                        {
                            diagonalWinCount++;
                            win = true;
                        } 

                        if(diagonalWinCount == Constants.DIAGONAL_LINES)
                        {
                            minorJackpot = true;
                        }             
                    }
                }

                //Check if jackpot was hit
                if(  
                    rowWinCount == Constants.ROWS &&
                    columnWinCount == Constants.COLUMNS && 
                    diagonalWinCount == Constants.DIAGONAL_LINES
                )
                {
                    jackpot = true;
                    Console.WriteLine($"\t JACKPOT!!! You won {bet * Constants.JACKPOT_MULTIPLIER}");
                    inputMoney += bet * Constants.JACKPOT_MULTIPLIER;
                }
                
                //Check if minor jackpot was hit
                if(minorJackpot && !jackpot)
                {
                    Console.WriteLine($"\t MINOR JACKPOT!!! You won {bet * Constants.MINOR_JACKPOT_MULTIPLIER}");
                    inputMoney += bet * Constants.MINOR_JACKPOT_MULTIPLIER;
                }
                
                //If win is true and jackpot is false multiply bet by WIN_MULTIPLIER
                if(win && !jackpot && !minorJackpot){
                    Console.WriteLine($"\t Winner!!! You won {bet * Constants.WIN_MULTIPLIER}");
                    inputMoney += bet * Constants.WIN_MULTIPLIER;
                }
        }

    }
}
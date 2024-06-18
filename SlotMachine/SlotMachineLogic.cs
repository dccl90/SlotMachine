namespace SlotMachine
{
    internal class SlotMachineLogic
    {
        
        static Random rnd = new Random();
        static int[,] numbers = new int[Constants.ROWS,Constants.COLUMNS];
        static bool jackpot = false;
        static bool minorJackpot = false;
        static char playingMode;
        static double bet;
        static double availableMoney;


        /// <summary>
        /// A method for fetching the available money variable
        /// </summary>
        ///<returns>A double that shows how much money is available to the player</returns>
        public static double GetAvailableMoney()
        {
            return availableMoney;
        }


        /// <summary>
        /// A method for fetching the bet variable
        /// </summary>
        ///<returns>A double value represnting the amount the player has bet</returns>
        public static double GetBet()
        {
            return bet;
        }

        /// <summary>
        /// A method for fetching the playing mode variable
        /// </summary>
        ///<returns>A char value represnting the game play mode</returns>
        public static char GetPlayingMode()
        {
            return playingMode;
        }

        /// <summary>
        /// A method for fetching the jackpot variable
        /// </summary>
        ///<returns>A boolean that determines if the player hit a jackpot</returns>
        public static bool GetJackpot()
        {
            return jackpot;
        }

        /// <summary>
        /// A method for fetching the minor jackpot variable
        /// </summary>
        ///<returns>A boolean that determines if the player hit a minor jackpot</returns>
        public static bool GetMinorJackpot()
        {
            return minorJackpot;
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
        /// Generates the random numbers for the slot machine
        /// </summary>
        ///<param name="money">Takes a double as the input for the money</param>
        public static void SetAvailableMoney(double money){
            availableMoney = money;
        }

        /// <summary>
        /// Sets the bet amount
        /// </summary>
        ///<param name="betAmount">Takes a double as the input for the bet amount</param>
        public static void SetBet(double betAmount)
        {
            bet = betAmount;
        }

        /// <summary>
        /// Sets the game mode
        /// </summary>
        ///<param name="mode">Takes a char as the input for the game mode</param>
        public static void SetPlayingMode(char mode)
        {
            playingMode = mode;
        }

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
        /// Checks if there is money available
        /// </summary>
        /// <returns>Returns a bool that determines if the player has money available</returns>
        public static bool IsMoneyAvailable()
        {
            if (availableMoney < Constants.MIN_BET)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if the playing mode is valid
        /// </summary>
        /// <param name="mode">The playing mode input by the player</param>
        /// <returns>Returns a bool that determines if the mode is valid</returns>
        public static bool ValidatePlayingMode(char mode)
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
        public static bool ValidateInputBet(string inputBet)
        {
            bool isBetValid = Double.TryParse(inputBet, out double bet);
            if(isBetValid && bet >= Constants.MIN_BET && bet <= availableMoney)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// A method to subtract the bet amount from the avaiable money
        /// </summary>
        /// <returns>Returns the available money minus the bet amount</returns>
        public static Double SubtractBetFromAvailableMoney()
        {
            return availableMoney -= bet;
        }

        /// <summary>
        /// A method for checking if the player has won
        /// </summary>
        ///<param name="win">A boolean value that represents if the player won</param>
        ///<return>Returns a double with the win amount won by the player</return>
        public static double MulitplyMoney(bool win)
        {
            if(jackpot)
            {
               return availableMoney += bet * Constants.JACKPOT_MULTIPLIER; 
            }

            if(minorJackpot)
            {
                return availableMoney += bet * Constants.MINOR_JACKPOT_MULTIPLIER; 
            }

            if(win)
            {
                return availableMoney += bet * Constants.WIN_MULTIPLIER;
            }

            return availableMoney;
        }

        /// <summary>
        /// A method for checking if the player has won
        /// </summary>
        /// <return>Returns a boolean value that determines if the player has won</return>
        public static bool CheckWin() 
        {
            bool win = false;
            int columnWinCount = 0;
            int rowWinCount = 0;
            int diagonalWinCount = 0;
                
            //Loop over Rows
            if(playingMode.Equals(Constants.ALL_ROWS) || playingMode.Equals(Constants.ALL_LINES))
            {
                rowWinCount = CheckHorizontalWin();
            }
            
            //Loop over Columns
            if(playingMode.Equals(Constants.ALL_COLUMNS) || playingMode.Equals(Constants.ALL_LINES))
            {
                columnWinCount = CheckVerticalWin();
            }

            //Loop over diagonal lines 
            if(playingMode.Equals(Constants.ALL_DIAGONALS) || playingMode.Equals(Constants.ALL_LINES)){
                diagonalWinCount = CheckDiagonalWin();
            }

            if(
                rowWinCount > 0 ||
                columnWinCount > 0 ||
                diagonalWinCount > 0
            )
            {
                win = true;
            }
            
            if(playingMode.Equals(Constants.ALL_LINES))
            {
                SetJackpot(rowWinCount, columnWinCount, diagonalWinCount);
            }
            
            return win;
        }

        /// <summary>
        /// A method for checking if the horizontal lines have matches
        /// </summary>
        /// <return>Returns an int with the number row win count</return>
        private static int CheckHorizontalWin()
        {
            int rowWinCheck;
            int rowWinCount = 0;
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
                }

                if(rowWinCount == Constants.ROWS)
                {
                    SetMinorJackpot(true);
                } 
                else 
                {
                    SetMinorJackpot(false);
                }   
            }
            return rowWinCount;
        }


        /// <summary>
        /// A method for checking if the vertical lines have matches
        /// </summary>
        /// <return>Returns an int with the number column win count</return>
        private static int CheckVerticalWin()
        {
            int columnWinCheck;
            int columnWinCount = 0;
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
                }

                if(columnWinCount == Constants.COLUMNS)
                {
                    SetMinorJackpot(true);
                } 
                else 
                {
                    SetMinorJackpot(false);
                }   
            }
            return columnWinCount;
        }

        /// <summary>
        /// A method for checking if the diagonal lines have matches
        /// </summary>
        /// <return>Returns an int with the number diagonal win count</return>
        private static int CheckDiagonalWin()
        {
            int diagonalWinCount = 0;
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
                } 

                if(diagonalWinCount == Constants.DIAGONAL_LINES)
                {
                    SetMinorJackpot(true);
                }   
                else 
                {
                    SetMinorJackpot(false);
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
                }  

                if(diagonalWinCount == Constants.DIAGONAL_LINES)
                {
                    SetMinorJackpot(true);
                } 
                else 
                {
                    SetMinorJackpot(false);
                }        
            }

            return diagonalWinCount;
        }

        /// <summary>
        /// A method for setting the minor jackpot variable
        /// </summary>
        private static void SetMinorJackpot(bool setMinorJackpot)
        {
            minorJackpot = setMinorJackpot;
        }

        /// <summary>
        /// A method for setting the jackpot variable
        /// </summary>
        private static void SetJackpot(int rowWinCount, int columnWinCount, int diagonalWinCount)
        {
            if(  
                rowWinCount == Constants.ROWS &&
                columnWinCount == Constants.COLUMNS && 
                diagonalWinCount == Constants.DIAGONAL_LINES &&
                playingMode.Equals(Constants.ALL_LINES)
            )
            {
                jackpot = true;
            } 
            else
            {
                jackpot = false;
            }  
        }
    }
}
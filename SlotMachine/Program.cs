using System.Collections.Immutable;
namespace SlotMachine
{
    internal class Program
    {

            const int ROWS = 3;
            const int COLUMNS = 3;
            const int RANGE_START = 1;
            const int RANGE_END = 10;
            const int ROW_ONE = 0;
            const int COLUMN_ONE = 0;
            const double MIN_BET = 1;
            const double WIN_MULTIPLIER = 2;
            const double JACKPOT_MULTIPLIER = 10;
            const double MINOR_JACKPOT_MULTIPLIER = 5;
            const char EXIT_GAME = 'n';
            const int CONST_ZERO = 0;
            const int CONST_ONE = 1;
            const int CONST_TWO = 2;
            const int CONST_THREE = 3;
        
        static void Main(string[] args)
        { 
            ImmutableList<char> gameModes = ImmutableList.Create('R', 'C', 'D', 'E');
            double money;
            char playingMode;
            Random rnd = new Random();
            int[,] numbers = new int[ROWS,COLUMNS]; 

            Console.Clear();
            
            while(true)
            {
                Console.Write("\t Add Money : $");
                string inputMoney = Console.ReadLine();
                bool isInputMoneyDouble = Double.TryParse(inputMoney, out money);
                //If input money isn't a double continue loop
                if(!isInputMoneyDouble || money < CONST_ONE)
                {
                    Console.WriteLine("\t Please enter a valid number");
                    continue;
                }

                //Break out of loop if none of the conditions are matched
                break;
            }

            while(true)
            {
                Console.WriteLine("\t Please enter your playing mode.");
                Console.WriteLine("\t Enter R to play all rows");
                Console.WriteLine("\t Enter C to play all columns");
                Console.WriteLine("\t Enter D to play all diagonals");
                Console.WriteLine("\t Enter E to play everything");
                Console.Write("\t Enter Mode: ");
                playingMode = Char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();
                if(!gameModes.Contains(playingMode))
                {
                    Console.Clear();
                    Console.WriteLine("\t Please enter R, C, D or E");
                    continue;
                }
                Console.Clear();
                break;
            }

            while(true)
            {
                Console.WriteLine("\t #####Lucky Dynasty Slots#####");
                Console.WriteLine($"\t Available Money: ${money}");
                Console.WriteLine($"\t Game Mode: {playingMode}");
                Console.Write("\t Enter Bet Amount {Min $1.00}: $");
                string inputBet = Console.ReadLine();
                bool isInputBetDouble = Double.TryParse(inputBet, out double bet);
                
                //If the input bet is not a double continue
                if(!isInputBetDouble){
                    Console.WriteLine("\t Please enter a valid number");
                    continue;
                }

                //If bet is less then the minimum then continue
                if(bet < MIN_BET){
                    Console.WriteLine("\t Bet must be greater then $1.00");
                    continue;
                }

                //If bet is greater then the available money continue
                if(bet > money)
                {
                    Console.WriteLine("\t Bet amount exceeds available money");
                    continue;
                }

                //Populate array and print thr table to the console
                for(int i = 0; i < numbers.GetLength(CONST_ZERO); i++)
                {
                    for(int j = 0; j < numbers.GetLength(CONST_ONE); j++)
                    {  
                        numbers[i,j] = rnd.Next(RANGE_START,RANGE_END);
                        Console.Write($"\t {numbers[i,j]} \t");
                    }
                    Console.WriteLine();
                }

                bool win = false;  
                bool jackpot = false;
                bool minorJackpot = false;
                int columnWinCheck;
                int rowWinCheck;
                int columnWinCount = 0;
                int rowWinCount = 0;
                int diagonalWinCount = 0;

                //Loop over Rows
                if(playingMode == gameModes[0] || playingMode == gameModes[3])
                {
                    for(int row = 0; row < ROWS; row++)
                    {   
                        rowWinCheck = 0; 
                        for(int column = 0; column < COLUMNS; column++)
                        {
                            
                            if(numbers[row,0] == numbers[row,column])
                            {
                                rowWinCheck++;
                            }

                            if(rowWinCheck == ROWS)
                            {
                                rowWinCount++;
                                win = true;
                            }

                            if(rowWinCount == ROWS)
                            {
                                minorJackpot = true;
                            }   
                        }
                    }
                }
                
                //Loop over Columns
                if(playingMode == gameModes[1] || playingMode == gameModes[3])
                {
                    for(int row = 0; row < ROWS; row++)
                    {   
                        columnWinCheck = 0;
                        for(int column = 0; column < COLUMNS; column++)
                        {

                            if(numbers[CONST_ZERO, row] == numbers[column,row])
                            {
                                columnWinCheck++;
                                
                            }
                            
                            if(columnWinCheck == COLUMNS)
                            {
                                columnWinCount++;
                                win = true;
                            }

                            if(columnWinCount == COLUMNS)
                            {
                                minorJackpot = true;
                            }   
                        }
                    }
                }

                //Loop over diagonal lines 
                if(playingMode == gameModes[2] || playingMode == gameModes[3]){
                    int diagonalWinCheck = 0;
                    for(int row = 0; row < ROWS; row++)
                    {
                        
                        if(numbers[ROW_ONE, COLUMN_ONE] == numbers[row,row]) 
                        {
                            diagonalWinCheck++;
                        }
                            
                        if(diagonalWinCheck == ROWS)
                        {
                            diagonalWinCount++;
                            win = true;
                        }              
                    }

                    diagonalWinCheck = 0;
                    for(int row = 0; row < ROWS; row++)
                    {
                        //Check diagonal win
                        if(numbers[ROW_ONE, COLUMNS - CONST_ONE] == numbers[row,COLUMNS - CONST_ONE - row]) 
                        {
                            diagonalWinCheck++;
                        }
                            
                        if(diagonalWinCheck == ROWS)
                        {
                            diagonalWinCount++;
                            win = true;
                        } 

                        if(diagonalWinCount == CONST_TWO)
                        {
                            minorJackpot = true;
                        }             
                    }
                }

                //Check if jackpot was hit
                if(  
                    rowWinCount == ROWS &&
                    columnWinCount == COLUMNS && 
                    diagonalWinCount == CONST_TWO
                )
                {
                    jackpot = true;
                    Console.WriteLine($"\t JACKPOT!!! You won {bet * JACKPOT_MULTIPLIER}");
                    money += bet * JACKPOT_MULTIPLIER;
                }
                
                //Check if minor jackpot was hit
                if(minorJackpot && !jackpot)
                {
                    Console.WriteLine($"\t MINOR JACKPOT!!! You won {bet * MINOR_JACKPOT_MULTIPLIER}");
                    money += bet * MINOR_JACKPOT_MULTIPLIER;
                }
                
                //If win is true and jackpot is false multiply bet by WIN_MULTIPLIER
                if(win && !jackpot && !minorJackpot){
                    Console.WriteLine($"\t Winner!!! You won {bet * WIN_MULTIPLIER}");
                    money += bet * WIN_MULTIPLIER;
                }

                //If valueCount is less then 3 subtract bet amount from the total money 
                if(!win)
                {
                    money -= bet;
                }

                //If money is less then 1, end the game.
                if(money < CONST_ONE)
                {
                    Console.WriteLine("\t No more Money Available");
                    break;
                }

                //Prompt user to place another bet
                Console.Write("\t Place another bet (Y/N): ");
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
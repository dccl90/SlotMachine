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
                SlotMachineLogic.GenerateNumbers();
                int[,] numbers = SlotMachineLogic.GetNumbers();
                UserInterface.PrintNumbers(numbers);
                bool win = false;  
                bool jackpot = false;
                bool minorJackpot = false;
                int columnWinCheck;
                int rowWinCheck;
                int columnWinCount = 0;
                int rowWinCount = 0;
                int diagonalWinCount = 0;

                //Loop over Rows
                if(playingMode.Equals(ALL_ROWS) || playingMode.Equals(ALL_LINES))
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
                
                //Loop over Columns
                if(playingMode.Equals(ALL_COLUMNS) || playingMode.Equals(ALL_LINES))
                {
                    for(int row = 0; row < ROWS; row++)
                    {   
                        columnWinCheck = 0;
                        for(int column = 0; column < COLUMNS; column++)
                        {
                            if(numbers[0, row] == numbers[column,row])
                            {
                                columnWinCheck++;       
                            }
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

                //Loop over diagonal lines 
                if(playingMode.Equals(ALL_DIAGONALS) || playingMode.Equals(ALL_LINES)){
                    int diagonalWinCheck = 0;
                    for(int row = 0; row < ROWS; row++)
                    {
                        
                        if(numbers[0, 0] == numbers[row,row]) 
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
                        if(numbers[0, COLUMNS - 1] == numbers[row,COLUMNS - 1 - row]) 
                        {
                            diagonalWinCheck++;
                        }
                            
                        if(diagonalWinCheck == ROWS)
                        {
                            diagonalWinCount++;
                            win = true;
                        } 

                        if(diagonalWinCount == DIAGONAL_LINES)
                        {
                            minorJackpot = true;
                        }             
                    }
                }

                //Check if jackpot was hit
                if(  
                    rowWinCount == ROWS &&
                    columnWinCount == COLUMNS && 
                    diagonalWinCount == DIAGONAL_LINES
                )
                {
                    jackpot = true;
                    Console.WriteLine($"\t JACKPOT!!! You won {bet * JACKPOT_MULTIPLIER}");
                    inputMoney += bet * JACKPOT_MULTIPLIER;
                }
                
                //Check if minor jackpot was hit
                if(minorJackpot && !jackpot)
                {
                    Console.WriteLine($"\t MINOR JACKPOT!!! You won {bet * MINOR_JACKPOT_MULTIPLIER}");
                    inputMoney += bet * MINOR_JACKPOT_MULTIPLIER;
                }
                
                //If win is true and jackpot is false multiply bet by WIN_MULTIPLIER
                if(win && !jackpot && !minorJackpot){
                    Console.WriteLine($"\t Winner!!! You won {bet * WIN_MULTIPLIER}");
                    inputMoney += bet * WIN_MULTIPLIER;
                }

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
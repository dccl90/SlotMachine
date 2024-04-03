using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;

namespace SlotMachine
{
    internal class Program
    {
        static void Main(string[] args)
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
            const char EXIT_GAME = 'n';
            const char SINGLE_LINE_MODE = 's';
            const char MULTI_LINE_MODE = 'm';
            const int CONST_TWO = 2;
            const int CONST_THREE = 3;
            
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
                if(!isInputMoneyDouble || money < 1)
                {
                    Console.WriteLine("\t Please enter a valid number");
                    continue;
                }

                //Break out of loop if none of the conditions are matched
                break;
            }

            while(true)
            {
                Console.Write("\t Press S for Signle Line Mode or M for Multi-Line Mode: ");
                playingMode = Char.ToLower(Console.ReadKey().KeyChar);
                Console.WriteLine();
                if(!playingMode.Equals(MULTI_LINE_MODE) && !playingMode.Equals(SINGLE_LINE_MODE))
                {
                    Console.WriteLine("\t Please enter S or M");
                    continue;
                }
                break;
            }

            while(true)
            {
                Console.WriteLine("\t #####Lucky Dynasty Slots#####");
                Console.WriteLine($"\t Available Money: ${money}");
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
                for(int i = 0; i < numbers.GetLength(0); i++)
                {
                    for(int j = 0; j < numbers.GetLength(1); j++)
                    {  
                        numbers[i,j] = rnd.Next(RANGE_START,RANGE_END);
                        Console.Write($"\t {numbers[i,j]} \t");
                    }
                    Console.WriteLine();
                }

                bool win = false;
                if(playingMode.Equals('s')){
                    int middleRowCheck = 0;
                    for(int row = 0; row < ROWS; row++)
                    {
                        if(numbers[((ROWS -1 ) / 2), COLUMN_ONE] == numbers[((ROWS -1 ) / 2), row])
                        {
                            middleRowCheck++;
                        }
                        else 
                        {
                            middleRowCheck = 0;
                        }

                        if(middleRowCheck == COLUMNS)
                        {
                            win = true;
                        }
                    }    
                }

                
                bool jackpot = false;
                int columnWinCheck;
                int rowWinCheck;
                int columnWinCount = 0;
                int rowWinCount = 0;
                int diagonalWinCount = 0;
                if(playingMode.Equals('m'))
                {
                    for(int row = 0; row < ROWS; row++)
                    {   
                        columnWinCheck = 0;
                        rowWinCheck = 0;
                        for(int column = 0; column < COLUMNS; column++)
                        {
                            //Check Columns for Win
                            if(numbers[row, 0] == numbers[column,row])
                            {
                                columnWinCheck++;
                            }

                            if(columnWinCheck == COLUMNS)
                            {
                                columnWinCount++;
                                win = true;
                            }

                            //Check Rows for Win
                            if(numbers[row,0] == numbers[row,column])
                            {
                                rowWinCheck++;
                            }

                            if(rowWinCheck == ROWS)
                            {
                                rowWinCount++;
                                win = true;
                            }
                        }
                    }

                    int diagonalWinCheck = 0;
                    for(int row = 0; row < ROWS; row++)
                    {
                        //Check diagonal win
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
                        if(numbers[ROW_ONE, COLUMNS - 1] == numbers[row,COLUMNS - 1 - row]) 
                        {
                            diagonalWinCheck++;
                        }
                          
                        if(diagonalWinCheck == ROWS)
                        {
                            diagonalWinCount++;
                            win = true;
                        }              
                    }
                    
                    //If all numbers match then set jackpot to true
                    if(rowWinCount == CONST_THREE && columnWinCount == CONST_THREE && diagonalWinCount == CONST_TWO){
                        jackpot = true;
                    }
                }
                
                //If win is true and jackpot is false multiply bet by WIN_MULTIPLIER
                if(win && jackpot)
                {
                    Console.WriteLine($"\t JACKPOT!!! You won {bet * JACKPOT_MULTIPLIER}");
                    money += bet * JACKPOT_MULTIPLIER;
                }
                
                //If win is true and jackpot is false multiply bet by WIN_MULTIPLIER
                if(win && !jackpot){
                    Console.WriteLine($"\t Winner!!! You won {bet * WIN_MULTIPLIER}");
                    money += bet * WIN_MULTIPLIER;
                }

                //If valueCount is less then 3 subtract bet amount from the total money 
                if(!win)
                {
                    money -= bet;
                }

                //If money is less then 1, end the game.
                if(money < 1)
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
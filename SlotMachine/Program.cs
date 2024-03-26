using System;
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
            const int RANGE_END = 3;
            const int ROW_ONE = 0;
            const int ROW_TWO = 1;
            const int ROW_THREE = 2;
            const int CELL_ONE = 0;
            const int CELL_TWO = 1;
            const int CELL_THREE = 2;
            const int VALUE_COUNT_MAX = 3;
            const double MIN_BET = 1;
            
            double money;
            double bet;
            Random rnd = new Random();
           int[,] numbers = new int[ROWS,COLUMNS]; 
            
            Console.Clear();
            
            while(true)
            {
                Console.Write("\t Add Money : $");
                string inputMoney = Console.ReadLine();
                bool isInputMoneyDouble = Double.TryParse(inputMoney, out money);
                //If input money isn't a double continue loop
                if(!isInputMoneyDouble)
                {
                    Console.WriteLine("\t Please enter a valid number");
                    continue;
                }

                //If money is less then 1 continue loop
                if(money < 1){
                    Console.WriteLine("\t Please enter a valid number");
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
                bool isInputBetDouble = Double.TryParse(inputBet, out bet);
                
                //If the input bet is not a double continue
                if(!isInputBetDouble){
                    Console.WriteLine("Please enter a valid number");
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

                
                int valueCount = 0;
                bool columnOneMatch = false;
                bool columnTwoMatch = false;
                bool columnThreeMatch = false;
                bool topRowMatch = false;
                bool middleRowMatch = false;
                bool bottomRowMatch = false;
                bool diagonalMatch = false;
                bool win = false;
                for(int i = 0; i < numbers.GetLength(0); i++)
                {   
                    //Check vertical match in the first column
                    for(int j = 0; j < numbers.GetLength(1); j++)
                    {
                        if(numbers[j,CELL_ONE] == numbers[ROW_ONE,CELL_ONE])
                        {
                            valueCount++;
                            if(valueCount == VALUE_COUNT_MAX)
                            {
                                valueCount = 0;
                                columnOneMatch = true;
                            }
                        }
                        else 
                        {
                            valueCount = 0;
                            break;
                        }
                    }

                    //Check vertical match in the second column
                    for(int j = 0; j < numbers.GetLength(1); j++)
                    {
                        if(numbers[j,CELL_TWO] == numbers[ROW_ONE,CELL_TWO])
                        {
                            valueCount++;
                            if(valueCount == VALUE_COUNT_MAX)
                            {
                                valueCount = 0;
                                columnTwoMatch = true;
                            }
                        }
                        else 
                        {
                            valueCount = 0;
                            break;
                        }
                    }

                    //Check vertical match in the third column
                    for(int j = 0; j < numbers.GetLength(1); j++)
                    {
                        if(numbers[j,CELL_THREE] == numbers[ROW_ONE,CELL_THREE])
                        {
                            valueCount++;
                            if(valueCount == VALUE_COUNT_MAX)
                            {
                                valueCount = 0;
                                columnOneMatch = true;
                            }
                        }
                        else 
                        {
                            valueCount = 0;
                            break;
                        }
                    }

                    //Check top row
                    for(int j = 0; j < numbers.GetLength(1); j++)
                    {
                        if(numbers[ROW_ONE,j] == numbers[ROW_ONE,CELL_ONE])
                        {     
                           valueCount++;
                            if(valueCount == VALUE_COUNT_MAX)
                            {
                                valueCount = 0;
                                topRowMatch = true;
                            }
                        }
                        else 
                        {
                            valueCount = 0;
                            break;
                        }
                    }
                    
                    //Check middle row
                    for(int j = 0; j < numbers.GetLength(1); j++)
                    {
                        if(numbers[ROW_TWO,j] == numbers[ROW_TWO,CELL_ONE])
                        {           
                            valueCount++;
                            if(valueCount == VALUE_COUNT_MAX)
                            {
                                valueCount = 0;
                                middleRowMatch = true;
                            }
                        }
                        else 
                        {
                            valueCount = 0;
                            break;
                        }
                    }

                    //Check bottom row
                    for(int j = 0; j < numbers.GetLength(1); j++)
                    {
                        if(numbers[ROW_THREE,j] == numbers[ROW_THREE,CELL_ONE])
                        {
                            valueCount++;
                            if(valueCount == VALUE_COUNT_MAX)
                            {
                                valueCount = 0;
                                bottomRowMatch = true;
                            }
                        }
                        else 
                        {
                            valueCount = 0;
                            break;
                        }
                    }

                    //Diagonal match
                    for(int j = 0; j < numbers.GetLength(1); j++)
                    {
                        if(numbers[ROW_TWO, CELL_TWO] == numbers[ROW_ONE,CELL_ONE] 
                        && numbers[ROW_THREE, CELL_THREE] == numbers[ROW_ONE,CELL_ONE])
                        {
                            diagonalMatch = true;
                        }

                        if(numbers[ROW_TWO, CELL_TWO] == numbers[ROW_ONE,CELL_THREE] 
                        && numbers[ROW_THREE, CELL_ONE] == numbers[ROW_ONE,CELL_THREE])
                        {
                            diagonalMatch = true;
                        } 
                    }

                    //If horrizontal, vertical and diagonal matches are found jackpot condition is met
                    if(topRowMatch && middleRowMatch && bottomRowMatch && diagonalMatch
                    && columnOneMatch && columnTwoMatch && columnThreeMatch)
                    {
                        win = true;
                        double winAmount = bet * 11;
                        money = money + winAmount;
                        Console.WriteLine($"\t Jackpot You won: ${winAmount}");
                        break;
                    }

                    if(topRowMatch || middleRowMatch || bottomRowMatch || diagonalMatch
                    || columnOneMatch || columnTwoMatch || columnThreeMatch)
                    {
                        win = true;
                        double winAmount = bet * 2;
                        money += winAmount;
                        Console.WriteLine($"\t You won: ${winAmount}");
                        break;
                    }
                }

                //If valueCount is less then 3 subtract bet amount from the total money 
                if(!win)
                {
                    money -= bet;
                }

                //If money is less then 1, end the game.
                if(money < 1)
                {
                    Console.WriteLine("No more Money Available");
                    break;
                }

                //Prompt user to place another bet
                Console.Write("\t Place another bet (Y/N): ");
                char betAgain = Console.ReadKey().KeyChar;
                if(betAgain.Equals('n'))
                {
                    break;
                }
                Console.Clear();
            }
        }
    }
}
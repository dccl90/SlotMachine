using System;
using System.Text.RegularExpressions;

namespace SlotMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int RANGE_START = 1;
            const int RANGE_END = 10;
            const int ROW_ONE = 1;
            const int FIRST_CELL = 1;
            const double MIN_BET = 1;
            
            double money;
            double bet;
            Random rnd = new Random();
            int[,] numbers = new int[3,3]; 
            
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
                int valueToMatch = numbers[ROW_ONE,FIRST_CELL];
                for(int i = 0; i < numbers.GetLength(0); i++)
                {   
                    for(int j = 0; j < numbers.GetLength(1); j++)
                    {
                        // If the numbers the second row are the same increase the count else set to 0
                        if(numbers[ROW_ONE,j] == valueToMatch)
                        {
                            valueCount++;
                        }
                        else 
                        {
                            valueCount = 0;
                        }
                    }

                    //If valueCount equqls 3, player wins and money is added to the available funds
                    if(valueCount == 3)
                    {
                        double winAmount = bet * 2;
                        money = money + winAmount;
                        Console.WriteLine($"\t You won: ${winAmount}");
                        break;
                    }
                }

                //If valueCount is less then 3 subtract bet amount from the total money 
                if(valueCount < 3)
                {
                    money = money - bet;
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
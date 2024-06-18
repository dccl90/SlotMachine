namespace SlotMachine
{
    internal class UserInterface
    {

        public static double InputMoney()
        {
            bool isInputValid = false;
            string inputMoney;
            do
            {
                Console.Write("\t Add Money : $");
                inputMoney = Console.ReadLine();
                isInputValid = SlotMachineLogic.ValidateInput(inputMoney);
                if(!isInputValid)
                {
                    PrintInvalidInputMessage();
                    continue;
                }
            } 
            while (!isInputValid);
            return Double.Parse(inputMoney);
        }

        private static void PrintInvalidInputMessage()
        {
            Console.WriteLine("\t Please enter a valid number");
        }
    }
}
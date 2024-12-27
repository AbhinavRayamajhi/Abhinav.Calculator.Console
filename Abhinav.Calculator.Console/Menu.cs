namespace Calculator
{
    class Menu
    {
        static double num1 = 0;
        static double num2 = 0;
        static Dictionary<string, string> OperatorKey= new()
        { {"a", "+"}, {"s", "-"}, {"m", "*"}, {"d", "/"}};
        public static void showMenu()
        {
            Console.Clear();
            Console.Write("Type a number, and then press Enter: ");
            num1 = Helper.ValidateUserInput(Console.ReadLine());
            Console.WriteLine();

            Console.Write("Type another number, and then press Enter: ");
            num2 = Helper.ValidateUserInput(Console.ReadLine());
            Console.WriteLine();

            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");

            string? userOperation = Console.ReadLine()?.ToLower();
            Console.WriteLine();

            while (userOperation != "a" && userOperation != "s" && userOperation != "m" && userOperation != "d")
            {
                Console.Write("That is an invalid option. Enter another operation: ");
                userOperation = Console.ReadLine()?.ToLower();
            }

            if (userOperation == "d")
            {
                while (num2 == 0)
                {
                    Console.WriteLine("Enter a non-zero divisor: ");
                    num2 = Helper.ValidateUserInput(Console.ReadLine());
                }
            }

            double result = Calculator.DoOperation(num1, num2, userOperation);

            Console.WriteLine();
            Console.WriteLine($"{num1} {OperatorKey[userOperation]} {num2} = {result}");

            Console.WriteLine("Press E to exit, Press any other key to continue.");
            if (Console.ReadLine()?.ToLower() == "e")
            {
                Environment.Exit(0);
            }
            else
            {
                showMenu();
            }
        }
    }
}

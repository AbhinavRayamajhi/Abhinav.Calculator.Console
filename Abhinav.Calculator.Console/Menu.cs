using System.Diagnostics.Metrics;

namespace Calculator
{
    class Menu
    {
        static double num1 = 0;
        static double num2 = 0;
        static Dictionary<string, string> OperatorKey= new()
        { {"a", "+"}, {"s", "-"}, {"m", "*"}, {"d", "/"}};

        public static void ShowMenu()
        {
            Console.Clear();

            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\tC - Calculate");
            Console.WriteLine("\tH - Game History");
            Console.WriteLine("\tE - Exit");
            Console.Write("Your option? ");

            string? userOption = Console.ReadLine()?.ToLower();
            Console.WriteLine();

            do
            {
                switch (userOption)
                {
                    case "c":
                        Calculate();
                        break;

                    case "h":
                        ShowGameHistory();
                        break;

                    case "e":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.Write("That is an invalid option. Enter another option: ");
                        userOption = Console.ReadLine()?.ToLower();
                        break;
                }
            } while (userOption != "c" || userOption != "h" || userOption != "e");
        }
        public static void Calculate()
        {
            Console.Clear();

            Console.Write("Type a number, and then press Enter: ");
            num1 = Helper.ValidateUserInput(Console.ReadLine());
            Console.WriteLine();

            Console.Write("Type another number, and then press Enter: ");
            num2 = Helper.ValidateUserInput(Console.ReadLine());
            Console.WriteLine();

            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\tA - Add");
            Console.WriteLine("\tS - Subtract");
            Console.WriteLine("\tM - Multiply");
            Console.WriteLine("\tD - Divide");
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

            string entry = $"{num1} {OperatorKey[userOperation]} {num2} = {result}";
            Console.WriteLine(entry);
            Helper.AddGameHistory(entry);
            Helper.IncreaseNumOfTimesPlayed();

            Console.WriteLine("\nPress E to exit to main menu, Press any other key to continue.");

            if (Console.ReadLine()?.ToLower() == "e")
            {
                ShowMenu();
            }
            else
            {
                Calculate();
            }
        }

        public static void ShowGameHistory()
        {
            Console.Clear();

            Helper.GameHistory();
            Console.Write("Press D to delete game history, E to exit the game or any other key to return to menu: ");

            string? historyInput = Console.ReadLine()?.ToString();

            if (historyInput?.ToLower() == "e")
            {
                Environment.Exit(0);
            }
            else if (historyInput?.ToLower() == "d")
            {
                DeleteHistory();
                ShowMenu();
            }
            else
            {
                ShowMenu();
            }
        }

        public static void DeleteHistory()
        {
            Console.WriteLine("\nChoose an option from the following list:");
            Console.WriteLine("\tA - Delete all history");
            Console.WriteLine("\tS - Delete specific history");
            Console.Write("Your option? ");

            string? userOption = Console.ReadLine()?.ToLower();

            Console.WriteLine();

            while (userOption != "a" && userOption != "s")
            {
                Console.Write("That is an invalid option. Enter another option: ");
                userOption = Console.ReadLine()?.ToLower();
            }

            switch (userOption)
            {
                case "a":
                    Helper.DeleteGameHistory();
                    return;

                case "s":
                    Console.Write("Enter the index of the history you want to delete: ");
                    int index = Convert.ToInt32(Console.ReadLine()) - 1;
                    Helper.DeleteSpecificGameHistory(index);
                    break;

                default:
                    break;
            }

            Console.Write("Enter D to delete more histories, Enter any other key to return to main menu...");

            if (Console.ReadLine()?.ToLower() == "d")
            {
                DeleteHistory();
            }
        }
    }
}

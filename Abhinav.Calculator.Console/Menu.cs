using System.Diagnostics.Metrics;
using System.Dynamic;

namespace Calculator
{
    class Menu
    {
        static double num1 = 0;
        static double num2 = 0;
        static Dictionary<string, string> OperatorKey= new()
        { {"a", "+"}, {"s", "-"}, {"m", "*"}, {"d", "/"}, {"p", "^" }, {"2", "Sqrt of"}, {"3", "Cube Root of" } };

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
                        userOption = GetOperator();
                        ShowResult(userOption);
                        break;

                    case "h":
                        ShowGameHistory();
                        break;

                    case "e":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.Write("That is an invalid option. Enter another option here: ");
                        userOption = Console.ReadLine()?.ToLower();
                        break;
                }
            } while (userOption != "c" || userOption != "h" || userOption != "e");
        }

        public static string GetOperator()
        {
            Console.Clear();

            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\tA - Add");
            Console.WriteLine("\tS - Subtract");
            Console.WriteLine("\tM - Multiply");
            Console.WriteLine("\tD - Divide");
            Console.WriteLine("\tP - Exponent / Power");
            Console.WriteLine("\t2 - Square Root");
            Console.WriteLine("\t3 - Cube Root");
            Console.Write("Your option? ");

            string? userOperation = Console.ReadLine()?.ToLower();
            Console.WriteLine();

            // check for valid operator input
            while (userOperation != "a" && userOperation != "s" && userOperation != "m" && userOperation != "d" && userOperation != "p" && userOperation != "2" && userOperation != "3") 
            {
                Console.Write("That is an invalid option. Enter another operation: ");
                userOperation = Console.ReadLine()?.ToLower();
            }

            return userOperation;
        }

        public static void ShowResult(string userOperation, List<double>? input = null)
        {
            double result;
            string entry;
            if (input == null)
            {
                input = GetInputNumbers(userOperation);
            }
            switch(double.TryParse(userOperation, out double a))
            {
                case true:
                    result = Calculator.OneDigitOperation(input[0], userOperation);

                    entry = $"{OperatorKey[userOperation]} ({input[0]}) = {result}";
                    break;

                case false:
                    result = Calculator.TwoDigitOperation(input[0], input[1], userOperation);

                    entry = $"{input[0]} {OperatorKey[userOperation]} {input[1]} = {result}";
                    break;
            }

            Console.WriteLine();

            input.Clear();
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
                string userOption = GetOperator();
                ShowResult(userOption);
            }
        }

        public static List<double> GetInputNumbers(string operation, double num1 = 0) // num1 is optional, the parameter is used if the user wants to use result from game history
        {
            double operationNum;
            List<double> input = new();

            if (num1 == 0)
            {
                Console.Write("Type first number and press Enter: ");
                num1 = Helper.ValidateUserInput(Console.ReadLine());
                input.Add(num1);
            }
            else
            {
                input.Add(num1);
            }

            if (double.TryParse(operation, out operationNum))
            {
                return input;
            }
            else
            { 
                Console.Write("\nType second number and press Enter: ");
                num2 = Helper.ValidateUserInput(Console.ReadLine());

                if (operation == "d")
                {
                    while (num2 == 0)
                    {
                        Console.WriteLine();
                        Console.Write("Enter a non-zero divisor: ");
                        num2 = Helper.ValidateUserInput(Console.ReadLine());
                    }
                }

                input.Add(num2);

                return input;
            }
        }

        public static void ShowGameHistory()
        {
            Console.Clear();

            Helper.GameHistory();
            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\tD - Delete Histories");
            Console.WriteLine("\tR - Reuse History for Calculation");
            Console.WriteLine("\tE - Exit the game");
            Console.WriteLine("\tAny other key to return to menu");
            Console.Write("Your option? ");

            string? historyInput = Console.ReadLine()?.ToString();

            if (historyInput?.ToLower() == "e")
            {
                Environment.Exit(0);
            }
            else if (historyInput?.ToLower() == "d")
            {
                DeleteHistory();
            }
            else if (historyInput?.ToLower() == "r")
            {
                UseHistory();
            }
            ShowMenu();
        }

        public static void UseHistory()
        {
            Console.Clear();
            Helper.GameHistory();

            Console.Write("\nEnter the index of the history you want to use: ");

            int index = Convert.ToInt32(Console.ReadLine()) - 1;
            string? historyEntry = Helper.ReturnHistoryAtIndex(index);

            if (historyEntry == null) //Check if history is empty
            {
                Console.WriteLine("Returning to Menu. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            string[] history = historyEntry.Split(" ");

            //Get result from history and convert to double
            double num1 = Convert.ToDouble(history[history.Length - 1]);

            string userOption = GetOperator();
            ShowResult(userOption, GetInputNumbers(userOption, num1));
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

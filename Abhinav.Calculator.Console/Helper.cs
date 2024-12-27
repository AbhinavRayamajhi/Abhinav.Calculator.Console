namespace Calculator
{
    class Helper
    {
        public static double ValidateUserInput(string? userInputString)
        {
            double result;
            while (!double.TryParse(userInputString, out result))
            {
                Console.WriteLine("Input a valid number: ");
                userInputString = Console.ReadLine();
            }
            return result;
        }
    }
}

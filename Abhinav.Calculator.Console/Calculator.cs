namespace Calculator
{
    class Calculator
    {
        public static double OneDigitOperation(double num, string op)
        {
            double result = double.NaN;

            switch (op)
            {
                case "2":
                    result = Math.Sqrt(num);
                    break;

                case "3":
                    result = Math.Cbrt(num);
                    break;

                default:
                    break;
            }
            return result;
        }

        public static double TwoDigitOperation(double num1, double num2, string op)
        {
            double result = double.NaN;

            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    break;

                case "s":
                    result = num1 - num2;
                    break;

                case "m":
                    result = num1 * num2;
                    break;

                case "d":
                    result = num1 / num2;
                    break;

                case "p":
                    result = Math.Pow(num1, num2);
                    break;

                default:
                    break;
            }
            return result;
        }
    }
}

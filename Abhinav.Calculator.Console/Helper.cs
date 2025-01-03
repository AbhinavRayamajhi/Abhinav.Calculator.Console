namespace Calculator
{
    class Helper
    {
        static int numOfTimesPlayed = 0;
        static List<string> history = new List<string>();

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

        public static void AddGameHistory(string historyEntry)
        {   
            string historyCount = (history.Count + 1).ToString();
            historyEntry = $"{historyCount}. {historyEntry}";
            history.Add(historyEntry);
        }

        public static void GameHistory()
        {
            Console.WriteLine($"Number of times played this session: {numOfTimesPlayed}\n");
            Console.WriteLine("Game History:\n");
            foreach (string entry in history)
            {
                Console.WriteLine(entry);
            }
            Console.WriteLine();
        }

        public static string? ReturnHistoryAtIndex(int index)
        {
            if (history.Count == 0)
            {
                Console.WriteLine("No history to return.");
                return null;
            }
            if (index < 0 || index >= history.Count)
            {
                Console.WriteLine("Invalid index.");
                return null;
            }
            return history[index];
        }

        public static void IncreaseNumOfTimesPlayed()
        {
            numOfTimesPlayed++;
        }

        public static void DeleteGameHistory()
        {
            history.Clear();
            Console.WriteLine("History Deleted. Press any key to continue to Menu...");
            Console.ReadKey();
        }

        public static void DeleteSpecificGameHistory(int index)
        {
            if (history.Count == 0)
            {
                Console.WriteLine("No history to delete.");
                return;
            }

            if (index < 0 || index >= history.Count)
            {
                Console.WriteLine("Invalid index. \n");
                return;
            }

            history.RemoveAt(index);

            for (int i = index; i < history.Count; i++) // Updating history with new index after deletion
            {
                string[] split = history[i].Split('.');

                int count = int.Parse(split[0]);
                count--;
                split[0] = count.ToString();

                history[i] = "";

                foreach (string s in split)
                {
                    history[i] += s;
                    if (Array.IndexOf(split, s) < split.Length - 1)
                    {
                        history[i] += ".";
                    }
                }
            }

            Console.Clear();
            Console.WriteLine("History Deleted. \n");

            GameHistory();
        }

    }
}

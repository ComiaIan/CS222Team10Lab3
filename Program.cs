using System;

class Program
{
    static void Main(string[] args)
    {
        Diary diary = new Diary();
        int choice;

        while (true)
        {
            Console.Write("===========================================");
            Console.WriteLine("\n               Diary Menu:");
            Console.Write("===========================================");
            Console.WriteLine("\n1. Write a New Entry");
            Console.WriteLine("2. View All Entries");
            Console.WriteLine("3. Search Entry by Date");
            Console.WriteLine("4. Exit");
            Console.Write("\nEnter your choice: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("\n===========================================");
                    Console.WriteLine("\n            Write a New Entry");
                    Console.Write("===========================================");
                    Console.Write("\nEnter your diary entry (end with an empty line):");
                    string entry = "";
                    string line;
                    while (!string.IsNullOrEmpty(line = Console.ReadLine()))
                    {
                        entry += line + Environment.NewLine;
                    }
                    diary.WriteEntry(entry);
                    break;

                case 2:
                    diary.ViewAllEntries();
                    break;

                case 3:
                    Console.Write("\n===========================================");
                    Console.WriteLine("\n            Search Entry Date");
                    Console.Write("===========================================\n");
                    Console.Write("Enter date to search (YYYY-MM-DD): ");
                    string date = Console.ReadLine();
                    diary.SearchByDate(date);
                    break;

                case 4:
                    return;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}

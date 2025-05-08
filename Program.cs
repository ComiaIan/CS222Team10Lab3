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
            Console.WriteLine("4. Delete Entry by Date");
            Console.WriteLine("5. Delete All Entries");
            Console.WriteLine("6. Exit");
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
                    Console.Write("\n===========================================");
                    Console.WriteLine("\n            Delete Entry Date");
                    Console.Write("===========================================\n");
                    Console.Write("Enter date to delete (YYYY-MM-DD): ");
                    string deleteDate = Console.ReadLine();
                    diary.DeleteEntry(deleteDate);
                    break;

                case 5:
                    Console.Write("\n===========================================");
                    Console.WriteLine("\n            Delete All Entries");
                    Console.Write("===========================================\n");
                    Console.Write("Are you sure you want to delete all entries? (y/n): ");
                    string confirm = Console.ReadLine();    
                    if (confirm.ToLower() == "y")
                    {
                        diary.ClearAllEntries();
                        Console.WriteLine("All entries deleted.");
                    }
                    else
                    {
                        Console.WriteLine("Operation cancelled.");
                    }
                    break;
                    
                case 6:
                    return;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}

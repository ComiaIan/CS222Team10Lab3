using System;

class Program
{
    public static void Main()
    {
        Diary diary = new Diary();
        bool search;

        diary.EnsureFileExists();

        while (true)
        {
            Console.Clear();
            Console.Write("===========================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n               DIARY MENU");
            Console.ResetColor();
            Console.Write("===========================================");
            Console.WriteLine("\n1. Write a New Entry");
            Console.WriteLine("2. View All Entries");
            Console.WriteLine("3. Search Entry by Date");
            Console.WriteLine("4. Delete Entry by Date");
            Console.WriteLine("5. Delete All Entries");
            Console.WriteLine("6. Exit");
            Console.Write("\nEnter your choice: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.Write("\n========================================================");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n                   Write a New Entry");
                    Console.ResetColor();
                    Console.WriteLine("========================================================");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Enter your diary entry (multiple lines allowed)");
                    Console.WriteLine("Type 'END' on a new line to finish, or 'CANCEL' to abort");
                    Console.ResetColor();
                    Console.WriteLine("--------------------------------------------------------");

                    string entry = "";
                    string? line;
                    bool add_entry = true;

                    while (true)
                    {
                        line = Console.ReadLine();
                        if (line?.ToUpper() == "CANCEL")
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\nEntry cancelled.");
                            Console.ResetColor();

                            add_entry = false;

                            diary.Pause();
                            break;
                        }
                        else if (line?.ToUpper() == "END")
                        {
                            if (line.Length == 3 && entry.Length == 0 || string.IsNullOrWhiteSpace(entry))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nCannot save empty entry. Add content or type 'CANCEL'\n");
                                Console.ResetColor();
                                add_entry = false;
                                continue;
                            }
                            break;
                        }
                        else
                        {
                            add_entry = true;
                            entry += line + Environment.NewLine;
                        }
                    }

                    if (add_entry)
                    {
                        diary.WriteEntry(entry);
                        diary.Pause();
                    }
                    break;

                case "2":
                    Console.Clear();
                    Console.Write("\n===========================================");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n               All Entries:");
                    Console.ResetColor();
                    Console.Write("===========================================\n");
                    diary.ViewAllEntries();
                    break;

                case "3":
                    search = true;

                    while (search)
                    {
                        Console.Clear();
                        Console.Write("\n===========================================");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\n            Search Entry by Date");
                        Console.ResetColor();
                        Console.Write("===========================================\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("(Enter 'CANCEL' to cancel the operation)");
                        Console.ResetColor();
                        Console.WriteLine("-------------------------------------------");
                        Console.Write("Enter date (yyyy-MM-dd): ");

                        string? dateInput = Console.ReadLine();

                        if (dateInput?.ToUpper() == "CANCEL")
                        {
                            search = false;
                            break;
                        }

                        diary.SearchByDate(dateInput);
                        search = diary.EnterDateAgain(search);
                    }
                    break;

                case "4":
                    search = true;
                    while (search)
                    {
                        Console.Clear();
                        Console.Write("\n===========================================");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\n           Delete Entry by Date");
                        Console.ResetColor();
                        Console.Write("===========================================\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("(Enter 'CANCEL' to cancel the operation)");
                        Console.ResetColor();
                        Console.WriteLine("-------------------------------------------");
                        Console.Write("Enter date (yyyy-MM-dd): ");

                        string? deleteDateInput = Console.ReadLine();

                        if (deleteDateInput?.ToUpper() == "CANCEL")
                        {
                            search = false;
                            break;
                        }

                        diary.DeleteEntry(deleteDateInput);
                        search = diary.EnterDateAgain(search);
                    }
                    break;

                case "5":
                    Console.Clear();
                    Console.Write("\n=====================================================");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n                 Delete All Entries");
                    Console.ResetColor();
                    Console.Write("=====================================================\n");
                    diary.ClearAllEntries();
                    break;

                case "6":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n[Exiting Diary. Goodbye!]");
                    Console.ResetColor();
                    Environment.Exit(0);
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice! Please enter 1-4.");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ResetColor();
                    Console.ReadKey();
                    break;
            }
        }
    }
}

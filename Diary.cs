using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

abstract class Diary
{
    public abstract void EnsureFileExists();
    public abstract void WriteEntry(string text);
    public abstract void ViewAllEntries();
    public abstract void SearchByDate(string date);
    public abstract void DeleteEntry(string targetDate);
    public abstract void ClearAllEntries();
    public abstract bool EnterDateAgain(bool search);
    public abstract bool IsValidDate(string date);
    public abstract void PrintDateInvalid();
    public abstract void Pause();


}
class Entry : Diary
{
    private readonly string filePath = "diary.txt";

    public override void EnsureFileExists()
    {
        if (!File.Exists(filePath))
        {
            using (File.Create(filePath)) { }
        }
    }

    public override void WriteEntry(string text)
    {
        try
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine($"{timestamp} | BEGIN ENTRY");
                writer.WriteLine(text);
                writer.WriteLine($"{timestamp} | END ENTRY");
                writer.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nEntry added successfully.");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Error saving entry: {ex.Message}]\n");
            Console.ResetColor();
        }
    }

    public override void ViewAllEntries()
    {
        try
        {
            if (new FileInfo(filePath).Length == 0)
            {
                Console.WriteLine("\n\n******************************\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nNo diary entries found.");
                Console.ResetColor();
                Pause();
                return;
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                int entryNumber = 1;
                string? line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("BEGIN ENTRY"))
                    {
                        if (entryNumber != 1)
                        {
                            Console.WriteLine("\n******************************");
                        }

                        Console.WriteLine($"\nEntry #{entryNumber++}:");
                        string date = line.Split('|')[0].Trim();
                        Console.WriteLine($"  Date: {date}");
                    }
                    else if (!line.Contains("END ENTRY") && !string.IsNullOrWhiteSpace(line))
                    {
                        Console.WriteLine("\t" + line);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Error saving entry: {ex.Message}]\n");
            Console.ResetColor();
        }
        Pause();
    }

    public override void SearchByDate(string date)
    {
        try
        {
            if (!IsValidDate(date))
            {
                PrintDateInvalid();
                return;
            }

            bool found = false;
            int entryCounter = 0;
            int matchedEntryCounter = 0;

            using (StreamReader reader = new StreamReader(filePath))
            {
                bool inEntry = false;
                bool isMatchingEntry = false;
                string currentDate = "";

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nSearch Results for {date}:");
                Console.ResetColor();
                Console.WriteLine("------------------------------");

                string? line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("BEGIN ENTRY"))
                    {
                        entryCounter++;
                        inEntry = true;
                        currentDate = line.Split('|')[0].Trim();
                        isMatchingEntry = currentDate.StartsWith(date);

                        if (isMatchingEntry)
                        {
                            found = true;
                            if (matchedEntryCounter != 0)
                            {
                                Console.WriteLine("\n******************************\n");
                            }
                            else
                            {
                                Console.WriteLine();
                            }
                            matchedEntryCounter++;
                            Console.WriteLine($"Entry #{entryCounter} - {currentDate}");
                        }
                    }
                    else if (inEntry && line.Contains("END ENTRY"))
                    {
                        inEntry = false;
                        isMatchingEntry = false;
                    }
                    else if (inEntry && isMatchingEntry && !string.IsNullOrWhiteSpace(line))
                    {
                        Console.WriteLine($"\t{line}");
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine("No entries found for the specified date.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nFound {matchedEntryCounter} entries matching {date}");
                Console.ResetColor();
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Error searching entries: {ex.Message}]\n");
            Console.ResetColor();
        }
    }

    public override void DeleteEntry(string targetDate)
    {
        if (!IsValidDate(targetDate))
        {
            PrintDateInvalid();
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filePath);
            List<string> updatedLines = new List<string>();

            bool tobeDeleted = false;
            string currentDate = "";
            int deletedCount = 0;

            foreach (string line in lines)
            {
                if (line.Contains("BEGIN ENTRY"))
                {
                    currentDate = line.Split('|')[0].Trim();
                    tobeDeleted = currentDate.StartsWith(targetDate);
                    if (tobeDeleted)
                        deletedCount++;
                }

                if (!tobeDeleted)
                {
                    updatedLines.Add(line);
                }

                if (line.Contains("END ENTRY"))
                {
                    tobeDeleted = false;
                }
            }

            if (deletedCount == 0)
            {
                Console.WriteLine("\nNo entries found for the specified date.");
            }
            else
            {
                if (updatedLines.Count == 1)
                {
                    using (StreamWriter writer = new StreamWriter(filePath, false))
                    {
                    }
                }
                else
                {
                    File.WriteAllLines(filePath, updatedLines);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n{deletedCount} entr{(deletedCount == 1 ? "y" : "ies")} successfully deleted dating {targetDate}.");
                Console.ResetColor();
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Error deleting entries: {ex.Message}]\n");
            Console.ResetColor();
        }
    }

    public override void ClearAllEntries()
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Are you sure you want to delete all entries? (y/n): ");
            Console.ResetColor();
            string? confirmDelete = Console.ReadLine();

            if (confirmDelete?.ToUpper() == "Y")
            {
                Console.WriteLine("\n\n******************************\n");

                if (new FileInfo(filePath).Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nNo diary entries found.");
                    Console.ResetColor();
                    Pause();
                    return;
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(filePath, false))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nAll entries cleared.");
                        Console.ResetColor();
                        Pause();
                        return;
                    }
                }
            }
            else if (confirmDelete?.ToUpper() == "N")
            {
                return;
            }
            else
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.\n");
                Console.ResetColor();
            }
        }
    }
    public override bool EnterDateAgain(bool search) // for search asking use if the user wants to search again in search by date
    {
        bool again = true;

        while (again)
        {
            Console.WriteLine("\n\n******************************\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nEnter another date? (y/n): ");
            Console.ResetColor();
            string? choice2 = Console.ReadLine()?.ToLower();
            if (choice2?.ToUpper() == "Y")
            {
                search = true;
                again = false;
            }
            else if (choice2?.ToUpper() == "N")
            {
                again = false;
                search = false;
            }
            else
            {
                again = true;
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                Console.ResetColor();
            }
        }
        Pause();
        return search;
    }

    public override bool IsValidDate(string date)
    {
        if (string.IsNullOrWhiteSpace(date) || date.Length != 10)
            return false;

        return DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _);
    }

    public override void PrintDateInvalid()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nInvalid date format. Please use yyyy-MM-dd.");
        Console.ResetColor();
        return;
    }

    public override void Pause()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\nPress any key to continue...");
        Console.ResetColor();
        Console.ReadKey();
    }
}

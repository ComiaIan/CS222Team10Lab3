//diary.cs

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

public class Diary
{
    private readonly string filePath = "diary.txt";

    public void EnsureFileExists()
    {
        if (!File.Exists(filePath))
        {
            using (File.Create(filePath)) { }
        }
    }

    public void WriteEntry(string text)
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

    public void ViewAllEntries()
    {
        Console.Clear();
        try
        {
            if (new FileInfo(filePath).Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\n\nNo entries found.");
                Console.ResetColor();
                Pause();
                return;
            }

            Console.Write("\n===========================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n               All Entries:");
            Console.ResetColor();
            Console.Write("===========================================\n");
            using (StreamReader reader = new StreamReader(filePath))
            {
                int entryNumber = 1;
                string? line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("BEGIN ENTRY"))
                    {
                        if(entryNumber != 1)
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

    public void SearchByDate(string date)
    {
        try
        {
            if (!IsValidDate(date))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid date format. Please use yyyy-MM-dd.");
                Console.ResetColor();
                Pause();
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

                Console.WriteLine($"\nSearch Results for {date}:");
                Console.WriteLine("--------------------------");

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
        Pause();
    }

    public bool SearchAgain(bool search)
    {
        bool again = true;

        while (again)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nSearch another date? (y/n): ");
            Console.ResetColor();
            string? choice2 = Console.ReadLine()?.ToLower();
            if (choice2 == "y")
            {
                search = true;
                again = false;
            }
            else if (choice2 == "n")
            {
                again = false;
                search = false;
            }
            else if (choice2 != "n" || choice2 == "n")
            {
                again = true;
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                Console.ResetColor();
            }
        }
        return search;
    }


    private bool IsValidDate(string date)
    {
        if (string.IsNullOrWhiteSpace(date) || date.Length != 10)
            return false;

        return DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _);
    }

    public void Pause()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\nPress any key to continue...");
        Console.ResetColor();
        Console.ReadKey();
    }

    public void DeleteEntry(string date)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("No diary entries found.");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            bool skipEntry = false;
            foreach (string line in lines)
            {
                if (line.StartsWith("Date: ") && line.Contains(date))
                {
                    skipEntry = true; // Skip this entry
                }
                else if (line == "---")
                {
                    skipEntry = false; // Reset for next entry
                }

                if (!skipEntry)
                {
                    writer.WriteLine(line);
                }
            }
        }
        Console.WriteLine("Entry deleted.");
    }
    public void ClearAllEntries()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Console.WriteLine("All entries cleared.");
        }
        else
        {
            Console.WriteLine("No diary entries found.");
        }
    }
}

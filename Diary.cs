using System;
using System.IO;

public class Diary
{
    private readonly string filePath = "diary.txt";

    static void Pause()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public void WriteEntry(string text)
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine($"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            writer.WriteLine(text);
            writer.WriteLine("---");
        }
        Console.WriteLine("Entry saved.\n");
    }

    public void ViewAllEntries()
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("No diary entries found.");
            return;
        }

        string content = File.ReadAllText(filePath);
        Console.Write("\n===========================================");
        Console.WriteLine("\n               All Entries:");
        Console.Write("===========================================\n");
        Console.WriteLine(content);
    }

    public void SearchByDate(string date)
    {
        if (!DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _))
        {
            Console.WriteLine("\nInvalid date. Please use the format yyyy-MM-dd.");
            return;
        }

        if (!File.Exists(filePath))
        {
            Console.WriteLine("No diary entries found.");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);
        bool foundEntry = false;
        bool withinMatchingEntry = false;

        foreach (string line in lines)
        {
            if (line.StartsWith("Date: "))
            {
                withinMatchingEntry = line.Contains(date);
                if (withinMatchingEntry)
                {
                    foundEntry = true;
                    Console.WriteLine(line);
                }
            }
            else if (withinMatchingEntry)
            {
                if (line == "---")
                {
                    withinMatchingEntry = false;
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(line);
                }
            }
        }

        if (!foundEntry)
        {
            Console.WriteLine("\nNo entries found for that date.");
        }
    }

}

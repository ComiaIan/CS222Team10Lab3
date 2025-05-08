using System;
using System.IO;

public class Diary
{
    private readonly string filePath = "diary.txt";

    public void WriteEntry(string text)
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine($"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            writer.WriteLine(text);
            writer.WriteLine("---");
        }
        Console.WriteLine("Entry saved.");
    }

    public void ViewAllEntries()
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("No diary entries found.");
            return;
        }

        string content = File.ReadAllText(filePath);
        Console.WriteLine("\nAll Entries:\n");
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

# 📖 Digital Diary - C# Console Application


## 📚 Overview of Contents
- [⚙ Project Description](#-project-description)
- [🔧 Features](#-features)
- [🕹 OOP Principles Used](#-oop-principles-used)
- [📄 How to Run the Application](#-how-to-run-the-application)
- [📝 File Structure](#-file-structure)
- [🖼 Sample Output](#-sample-output)
- [👥 Team Members](#-team-members)
- [📃 Acknowledgement](#-acknowledgement)

---

## ⚙ Project Description

**Digital Diary** is a modular, console-based C# application that allows users to maintain a personal diary. Users can:
- Write new diary entries
- View all saved entries
- Search entries by date
- Delete by date
- Clear all entries

Entries are stored in a local text file (`diary.txt`) using file handling techniques, ensuring that all data will be saved and can be used  across sessions. This project demonstrates a clean implementation of object-oriented programming (OOP) principles in C#.

## 🔧 Features

- **Write Entries:** Add new entries with timestamped logs.
- **View Entries:** Display all past entries saved in the diary.
- **Search by Date:** Find specific entries using a YYYY-MM-DD formatted date.
- **Delete by Date:** Remove diary entries for a given date.
- **Clear All Entries:** Erase all saved entries in one command.
- **File Persistence:** All data is stored in a text file using `StreamWriter` and `File.ReadAllText`.
- **Error Handling.** Handles invalid inputs and provides user feedback.


## 🕹 OOP Principles Used

- **Encapsulation:** The Diary class encapsulates the filePath field and related operations (WriteEntry, ViewAllEntries, and SearchByDate) to manage diary entries. The filePath is marked as private readonly, which hides it from external classes and prevents modification, protecting the integrity of the data.

- **Abstraction:** The Diary class provides simple public methods to write, view, and search entries without exposing how those operations interact with the file system. The user of the class doesn’t need to understand file handling to use the diary functionality.


- **Modularity:** Functionality is separated into `Diary.cs` for logic and `Program.cs` for user interaction and flow. The Diary class is a self-contained module for diary management. The Program class is responsible for user interaction and control flow, keeping concerns separate from diary logic.


## 📄 How to Run the Application

1. Open the project in **Visual Studio** or any C#-compatible IDE.
2. Ensure the following files are present:
   - `Program.cs`
   - `Diary.cs`
3. Build the solution.
4. Run the application.
5. Follow the menu to write, view, or search diary entries.

## 📝 File Structure

```
Diary/
├── bin/
│   └── Debug/
│       └── net9.0/
├── obj/
├── Diary.cs
├── Lab 3.csproj
├── Program.cs
├── README.md
└── diary.txt
```

## 🖼 Sample Output

```
===========================================
               DIARY MENU
===========================================
1. Write a New Entry
2. View All Entries
3. Search Entry by Date
4. Delete Entry by Date
5. Delete All Entries
6. Exit

Enter your choice: 1

========================================================
                   Write a New Entry
========================================================
Enter your diary entry (multiple lines allowed)
Type 'END' on a new line to finish, or 'CANCEL' to abort
--------------------------------------------------------
Today was a productive day.
I worked on several programming tasks and completed them on time.
Had a good cup of coffee too!
END

Entry added successfully.

Press any key to continue...
```

```
===========================================
               DIARY MENU
===========================================
1. Write a New Entry
2. View All Entries
3. Search Entry by Date
4. Delete Entry by Date
5. Delete All Entries
6. Exit

Enter your choice: 2

===========================================
               All Entries:
===========================================

Entry #1:
  Date: 2025-05-07 10:51:24
        Today was a productive day.
        I worked on several programming tasks and completed them on time.
        Had a good cup of coffee too!

******************************

Entry #2:
  Date: 2025-05-08 14:35:46
        My favorite shoes got muddy today because it rained hard early this morning.

Press any key to continue...
```

```
===========================================
            Search Entry by Date
===========================================
(Enter 'CANCEL' to cancel the operation)
-------------------------------------------
Enter date (yyyy-MM-dd): 2025-05-07

Search Results for 2025-05-07:
------------------------------

Entry #1 - 2025-05-07
        Today was a productive day.
        I worked on several programming tasks and completed them on time.
        Had a good cup of coffee too!

Found 1 entries matching 2025-05-07


******************************


Enter another date? (y/n): n

Press any key to continue...
```

```
===========================================
            Delete Entry by Date
===========================================
(Enter 'CANCEL' to cancel the operation)
-------------------------------------------
Enter date (yyyy-MM-dd): 2025-05-07

1 entry successfully deleted dating 2025-05-07.


******************************


Enter another date? (y/n): n

Press any key to continue...
```

```
=====================================================
                 Delete All Entries
=====================================================
Are you sure you want to delete all entries? (Y/N): y


******************************


All entries cleared.

Press any key to continue...
```

```
[Exiting Diary. Goodbye!]
```
## 👥 Team Members

- Alcaraz John  
- Bulanadi Sophia Loureine  
- Comia Ian Emmanuel  
- Niepes Marc Vergel  

## 📃 Acknowledgement

Special thanks to our Advance Object-Oriented Programming Ms. Marie Agdon for guiding us in understanding the C# language and object-oriented programming concepts. We would also like to give our gratitude to all of our supportive classmates and parents that motivate us in making this project come into  a success. They all helped us push further and be able to apply our learning in  real-world scenarios.


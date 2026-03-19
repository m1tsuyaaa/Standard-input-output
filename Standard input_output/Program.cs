using System;

class Program
{
  static void Main(string[] args)
  {
    string userChoice;

    while (true)
    {
      Console.Clear();
      Console.WriteLine("=== Main Menu ===");
      Console.WriteLine("1. Text Editor");
      Console.WriteLine("2. File Indexer");
      Console.WriteLine("3. Exit");
      Console.Write("Select application: ");

      userChoice = Console.ReadLine();

      if (userChoice == "1")
      {
        ConsoleTextEditor textEditor;
        textEditor = new ConsoleTextEditor();
        textEditor.Run();
      }
      else if (userChoice == "2")
      {
        FileIndexerApplication fileIndexer;
        fileIndexer = new FileIndexerApplication();
        fileIndexer.Run();
      }
      else if (userChoice == "3")
      {
        return;
      }
      else
      {
        Console.WriteLine("Invalid choice.");
        Console.ReadKey();
      }
    }
  }
using System;

class Program
{
  static string UserChoice;
  static ConsoleTextEditor TextEditor;
  static FileIndexerApplication FileIndexer;

  static void Main(string[] args)
  {
    while (true)
    {
      Console.Clear();
      Console.WriteLine("=== Main Menu ===");
      Console.WriteLine("1. Text Editor");
      Console.WriteLine("2. File Indexer");
      Console.WriteLine("3. Exit");
      Console.Write("Select application: ");

      UserChoice = Console.ReadLine();

      if (UserChoice == "1")
      {
        TextEditor = new ConsoleTextEditor();
        TextEditor.Run();
      }
      else if (UserChoice == "2")
      {
        FileIndexer = new FileIndexerApplication();
        FileIndexer.Run();
      }
      else if (UserChoice == "3")
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
}
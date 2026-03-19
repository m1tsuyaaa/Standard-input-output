using System;
using System.IO;

public class ConsoleTextEditor
{
  public TextDocument CurrentDocument;
  public DocumentHistory DocumentHistory;

  public void Run()
  {
    while (true)
    {
      DisplayMainMenu();
      string userInput;
      userInput = Console.ReadLine();

      if (userInput == "1")
      {
        OpenDocument();
      }
      else if (userInput == "2")
      {
        CreateNewDocument();
      }
      else if (userInput == "3")
      {
        EditDocument();
      }
      else if (userInput == "4")
      {
        return;
      }
      else
      {
        Console.WriteLine("Invalid choice.");
      }
    }
  }

  public void DisplayMainMenu()
  {
    Console.Clear();
    Console.WriteLine("=== Text Editor ===");
    Console.WriteLine("1. Open document");
    Console.WriteLine("2. Create new document");
    Console.WriteLine("3. Edit document");
    Console.WriteLine("4. Save document");
    Console.WriteLine("5. Exit");

    if (CurrentDocument != null)
    {
      Console.WriteLine($"\nCurrent document: {CurrentDocument.FileName}");
    }

    Console.Write("\nSelect action: ");
  }

  public void OpenDocument()
  {
    Console.Write("Enter file path: ");
    string filePath;
    filePath = Console.ReadLine();

    if (File.Exists(filePath))
    {
      CurrentDocument = new TextDocument(filePath);
      DocumentHistory = new DocumentHistory(CurrentDocument);
      Console.WriteLine("Document loaded.");
    }
    else
    {
      Console.WriteLine("File not found.");
    }

    Console.WriteLine("Press any key...");
    Console.ReadKey();
  }

  public void CreateNewDocument()
  {
    Console.Write("Enter path for new file: ");
    string filePath;
    filePath = Console.ReadLine();

    CurrentDocument = new TextDocument(filePath)
    {
      Content = string.Empty
    };

    DocumentHistory = new DocumentHistory(CurrentDocument);
    Console.WriteLine("New document created.");
    Console.WriteLine("Press any key...");
    Console.ReadKey();
  }

  public void EditDocument()
  {
    if (CurrentDocument == null)
    {
      Console.WriteLine("Open a document first.");
      Console.WriteLine("Press any key...");
      Console.ReadKey();
      return;
    }

    bool editing;
    editing = true;

    int fiftyPercentValue;
    fiftyPercentValue = 50;

    while (editing)
    {
      Console.Clear();
      Console.WriteLine("Editing");
      Console.WriteLine(new string('-', fiftyPercentValue));
      Console.WriteLine(CurrentDocument.Content);
      Console.WriteLine(new string('-', fiftyPercentValue));
      Console.WriteLine(":w - save and exit");
      Console.WriteLine(":q - exit without saving");
      Console.WriteLine(":u - undo");
      Console.WriteLine(":r - redo");

      string userInput;
      userInput = Console.ReadLine();

      if (userInput == ":w")
      {
        CurrentDocument.SaveToFile();
        DocumentHistory.SaveState();
        editing = false;
      }
      else if (userInput == ":q")
      {
        editing = false;
      }
      else if (userInput == ":u")
      {
        DocumentHistory.Undo();
      }
      else if (userInput == ":r")
      {
        DocumentHistory.Redo();
      }
      else
      {
        CurrentDocument.Content += userInput + Environment.NewLine;
        DocumentHistory.SaveState();
      }
    }
  }
}
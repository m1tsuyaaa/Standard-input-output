using System;
using System.IO;

public class ConsoleTextEditor
{
  public TextDocument CurrentDocument;
  public DocumentHistory DocumentHistory;
  public string UserInput;
  public string FilePath;
  public bool Editing;
  public int FiftyPercentValue;
  public string EditUserInput;

  public void Run()
  {
    while (true)
    {
      DisplayMainMenu();
      UserInput = Console.ReadLine();

      if (UserInput == "1")
      {
        OpenDocument();
      }
      else if (UserInput == "2")
      {
        CreateNewDocument();
      }
      else if (UserInput == "3")
      {
        EditDocument();
      }
      else if (UserInput == "4")
      {
        SaveDocument();
      }
      else if (UserInput == "5")
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
    FilePath = Console.ReadLine();

    if (File.Exists(FilePath))
    {
      CurrentDocument = new TextDocument(FilePath);
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
    FilePath = Console.ReadLine();

    CurrentDocument = new TextDocument(FilePath)
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

    Editing = true;
    FiftyPercentValue = 50;

    while (Editing)
    {
      Console.Clear();
      Console.WriteLine("Editing");
      Console.WriteLine(new string('-', FiftyPercentValue));
      Console.WriteLine(CurrentDocument.Content);
      Console.WriteLine(new string('-', FiftyPercentValue));
      Console.WriteLine(":w - save and exit");
      Console.WriteLine(":q - exit without saving");
      Console.WriteLine(":u - undo");
      Console.WriteLine(":r - redo");

      EditUserInput = Console.ReadLine();

      if (EditUserInput == ":w")
      {
        CurrentDocument.SaveToFile();
        DocumentHistory.SaveState();
        Editing = false;
      }
      else if (EditUserInput == ":q")
      {
        Editing = false;
      }
      else if (EditUserInput == ":u")
      {
        DocumentHistory.Undo();
      }
      else if (EditUserInput == ":r")
      {
        DocumentHistory.Redo();
      }
      else
      {
        CurrentDocument.Content += EditUserInput + Environment.NewLine;
        DocumentHistory.SaveState();
      }
    }
  }

  public void SaveDocument()
  {
    if (CurrentDocument == null)
    {
      Console.WriteLine("No document to save.");
    }
    else
    {
      CurrentDocument.SaveToFile();
      Console.WriteLine("Document saved.");
    }

    Console.WriteLine("Press any key...");
    Console.ReadKey();
  }
}
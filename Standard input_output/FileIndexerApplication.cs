using System;
using System.Collections.Generic;
using System.IO;

public class FileIndexerApplication
{
  public DocumentSearcher DocumentSearcher;
  public string IndexDirectory;
  public string UserChoice;
  public string KeywordInput;
  public List<string> Keywords;
  public int Index;
  public string CaseSensitiveInput;
  public bool CaseSensitive;
  public List<TextDocument> SearchResults;
  public int ResultIndex;
  public TextDocument CurrentDocument;
  public int BaseNumber;
  public int DisplayNumber;
  public int DocIndex;

  public void Run()
  {
    Console.WriteLine("Text File Indexer");
    Console.Write("Enter directory path: ");
    IndexDirectory = Console.ReadLine();

    if (!Directory.Exists(IndexDirectory))
    {
      Console.WriteLine("Directory does not exist.");
      return;
    }

    DocumentSearcher = new DocumentSearcher(IndexDirectory);

    Console.WriteLine("Indexing files...");
    DocumentSearcher.IndexAll();

    Console.WriteLine($"Files indexed: {DocumentSearcher.Documents.Count}");

    while (true)
    {
      DisplayIndexerMenu();
      UserChoice = Console.ReadLine();

      if (UserChoice == "1")
      {
        PerformKeywordSearch();
      }
      else if (UserChoice == "2")
      {
        ReindexDirectory();
      }
      else if (UserChoice == "3")
      {
        DisplayAllDocuments();
      }
      else if (UserChoice == "4")
      {
        return;
      }
      else
      {
        Console.WriteLine("Invalid choice.");
      }
    }
  }

  public void DisplayIndexerMenu()
  {
    Console.WriteLine("\n=== Indexer Menu ===");
    Console.WriteLine("1. Search by keywords");
    Console.WriteLine("2. Reindex");
    Console.WriteLine("3. Show all documents");
    Console.WriteLine("4. Exit");
    Console.Write("Select action: ");
  }

  public void PerformKeywordSearch()
  {
    Console.Write("Enter keywords separated by commas: ");
    KeywordInput = Console.ReadLine();

    Keywords = new List<string>(
        KeywordInput.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
    );

    for (Index = 0; Index < Keywords.Count; ++Index)
    {
      Keywords[Index] = Keywords[Index].Trim();
    }

    Console.Write("Case sensitive? (y/n): ");
    CaseSensitiveInput = Console.ReadLine();
    CaseSensitive = CaseSensitiveInput?.ToLower() == "y";

    SearchResults = DocumentSearcher.Search(Keywords, CaseSensitive);

    Console.WriteLine($"\nDocuments found: {SearchResults.Count}");

    BaseNumber = 1;

    for (ResultIndex = 0; ResultIndex < SearchResults.Count; ++ResultIndex)
    {
      CurrentDocument = SearchResults[ResultIndex];
      DisplayNumber = ResultIndex + BaseNumber;

      Console.WriteLine($"{DisplayNumber}. {CurrentDocument.FileName}");
    }

    Console.WriteLine("\nPress any key...");
    Console.ReadKey();
  }

  public void ReindexDirectory()
  {
    Console.WriteLine("Reindexing...");
    DocumentSearcher.IndexAll();
    Console.WriteLine($"Files: {DocumentSearcher.Documents.Count}");
    Console.WriteLine("Press any key...");
    Console.ReadKey();
  }

  public void DisplayAllDocuments()
  {
    Console.WriteLine("\n=== All Documents ===");

    for (DocIndex = 0; DocIndex < DocumentSearcher.Documents.Count; ++DocIndex)
    {
      CurrentDocument = DocumentSearcher.Documents[DocIndex];
      Console.WriteLine($"{DocIndex + 1}. {CurrentDocument.FileName}");
      Console.WriteLine($"   Path: {CurrentDocument.FilePath}");
      Console.WriteLine($"   Size: {CurrentDocument.Content.Length} chars");
      Console.WriteLine();
    }

    Console.WriteLine("Press any key...");
    Console.ReadKey();
  }
}
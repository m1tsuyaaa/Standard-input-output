using System;
using System.Collections.Generic;
using System.IO;

public class FileIndexerApplication
{
  public DocumentSearcher DocumentSearcher;
  public string IndexDirectory;

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

    string userChoice;

    while (true)
    {
      DisplayIndexerMenu();
      userChoice = Console.ReadLine();

      if (userChoice == "1")
      {
        PerformKeywordSearch();
      }
      else if (userChoice == "2")
      {
        ReindexDirectory();
      }
      else if (userChoice == "3")
      {
        DisplayAllDocuments();
      }
      else if (userChoice == "4")
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
    string keywordInput;
    keywordInput = Console.ReadLine();

    List<string> keywords;
    keywords = new List<string>(
        keywordInput.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
    );

    int index;
    for (index = 0; index < keywords.Count; ++index)
    {
      keywords[index] = keywords[index].Trim();
    }

    Console.Write("Case sensitive? (y/n): ");
    string caseSensitiveInput;
    caseSensitiveInput = Console.ReadLine();

    bool caseSensitive;
    caseSensitive = caseSensitiveInput?.ToLower() == "y";

    List<TextDocument> searchResults;
    searchResults = DocumentSearcher.Search(keywords, caseSensitive);

    Console.WriteLine($"\nDocuments found: {searchResults.Count}");

    int resultIndex;
    TextDocument currentDocument;

    int baseNumber;
    baseNumber = 1;

    int displayNumber;
    int baseNumber;
    baseNumber = 1;

    for (resultIndex = 0; resultIndex < searchResults.Count; ++resultIndex)
    {
      currentDocument = searchResults[resultIndex];
      displayNumber = resultIndex + baseNumber;

      Console.WriteLine($"{displayNumber}. {currentDocument.FileName}");
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

    int docIndex;
    TextDocument currentDocument;

    for (docIndex = 0; docIndex < DocumentSearcher.Documents.Count; ++docIndex)
    {
      currentDocument = DocumentSearcher.Documents[docIndex];
      Console.WriteLine($"{docIndex + 1}. {currentDocument.FileName}");
      Console.WriteLine($"   Path: {currentDocument.FilePath}");
      Console.WriteLine($"   Size: {currentDocument.Content.Length} chars");
      Console.WriteLine();
    }

    Console.WriteLine("Press any key...");
    Console.ReadKey();
  }
}
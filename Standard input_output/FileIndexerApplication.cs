using System;
using System.Collections.Generic;
using System.IO;

public class FileIndexerApplication
{
  public DocumentSearcher DocumentSearcher;
  public string IndexDirectory;

  public void Run()
  {
    Console.WriteLine("=== Индексатор текстовых файлов ===");
    Console.Write("Введите путь к директории: ");
    IndexDirectory = Console.ReadLine();

    if (!Directory.Exists(IndexDirectory))
    {
      Console.WriteLine("Директория не существует.");
      return;
    }

    DocumentSearcher = new DocumentSearcher(IndexDirectory);

    Console.WriteLine("Индексация файлов...");
    DocumentSearcher.IndexAll();

    Console.WriteLine($"Проиндексировано файлов: {DocumentSearcher.Documents.Count}");

    while (true)
    {
      DisplayIndexerMenu();
      string userChoice;
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
        Console.WriteLine("Неверный выбор.");
      }
    }
  }

  public void DisplayIndexerMenu()
  {
    Console.WriteLine("\n=== Меню индексатора ===");
    Console.WriteLine("1. Поиск по ключевым словам");
    Console.WriteLine("2. Переиндексировать");
    Console.WriteLine("3. Показать все документы");
    Console.WriteLine("4. Выход");
    Console.Write("Выберите действие: ");
  }

  public void PerformKeywordSearch()
  {
    Console.Write("Введите ключевые слова через запятую: ");
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

    Console.Write("Учитывать регистр? (y/n): ");
    string caseSensitiveInput;
    caseSensitiveInput = Console.ReadLine();

    bool caseSensitive;
    caseSensitive = caseSensitiveInput?.ToLower() == "y";

    List<TextDocument> searchResults;
    searchResults = DocumentSearcher.Search(keywords, caseSensitive);

    Console.WriteLine($"\nНайдено документов: {searchResults.Count}");

    int resultIndex;
    TextDocument currentDocument;

    int baseNumber;
    baseNumber = 1;

    for (resultIndex = 0; resultIndex < searchResults.Count; ++resultIndex)
    {
      currentDocument = searchResults[resultIndex];

      int displayNumber;
      displayNumber = resultIndex + baseNumber;

      Console.WriteLine($"{displayNumber}. {currentDocument.FileName}");
    }

    Console.WriteLine("\nНажмите любую клавишу...");
    Console.ReadKey();
  }

  public void ReindexDirectory()
  {
    Console.WriteLine("Переиндексация...");
    DocumentSearcher.IndexAll();
    Console.WriteLine($"Файлов: {DocumentSearcher.Documents.Count}");
    Console.WriteLine("Нажмите любую клавишу...");
    Console.ReadKey();
  }

  public void DisplayAllDocuments()
  {
    Console.WriteLine("\n=== Все документы ===");

    int docIndex;
    TextDocument currentDocument;

    for (docIndex = 0; docIndex < DocumentSearcher.Documents.Count; ++docIndex)
    {
      currentDocument = DocumentSearcher.Documents[docIndex];
      Console.WriteLine($"{docIndex + 1}. {currentDocument.FileName}");
      Console.WriteLine($"   Путь: {currentDocument.FilePath}");
      Console.WriteLine($"   Размер: {currentDocument.Content.Length} симв.");
      Console.WriteLine();
    }

    Console.WriteLine("Нажмите любую клавишу...");
    Console.ReadKey();
  }
}
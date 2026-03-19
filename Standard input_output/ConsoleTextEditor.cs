using System;
using System.IO;

public class ConsoleTextEditor
{
  public TextDocument currentDocument;
  public DocumentHistory documentHistory;

  public void Run()
  {
    while (true)
    {
      DisplayMainMenu();
      string userInput = Console.ReadLine();

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
        Console.WriteLine("Неверный выбор.");
      }
    }
  }

  public void DisplayMainMenu()
  {
    Console.Clear();
    Console.WriteLine("=== Текстовый редактор ===");
    Console.WriteLine("1. Открыть документ");
    Console.WriteLine("2. Создать новый документ");
    Console.WriteLine("3. Редактировать документ");
    Console.WriteLine("4. Сохранить документ");
    Console.WriteLine("5. Выход");

    if (currentDocument != null)
    {
      Console.WriteLine($"\nТекущий документ: {currentDocument.FileName}");
    }

    Console.Write("\nВыберите действие: ");
  }

  public void OpenDocument()
  {
    Console.Write("Введите путь к файлу: ");
    string filePath = Console.ReadLine();

    if (File.Exists(filePath))
    {
      currentDocument = new TextDocument(filePath);
      documentHistory = new DocumentHistory(currentDocument);
      Console.WriteLine("Документ загружен.");
    }
    else
    {
      Console.WriteLine("Файл не найден.");
    }

    Console.WriteLine("Нажмите любую клавишу...");
    Console.ReadKey();
  }

  public void CreateNewDocument()
  {
    Console.Write("Введите путь для нового файла: ");
    string filePath = Console.ReadLine();

    currentDocument = new TextDocument(filePath)
    {
      content = string.Empty
    };

    documentHistory = new DocumentHistory(currentDocument);
    Console.WriteLine("Новый документ создан.");
    Console.WriteLine("Нажмите любую клавишу...");
    Console.ReadKey();
  }

  public void EditDocument()
  {
    if (currentDocument == null)
    {
      Console.WriteLine("Сначала откройте документ.");
      Console.WriteLine("Нажмите любую клавишу...");
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
      Console.WriteLine("Редактирование");
      Console.WriteLine(new string('-', fiftyPercentValue));
      Console.WriteLine(currentDocument.content);
      Console.WriteLine(new string('-', fiftyPercentValue));
      Console.WriteLine(":w - сохранить и выйти");
      Console.WriteLine(":q - выйти без сохранения");
      Console.WriteLine(":u - отменить");
      Console.WriteLine(":r - повторить");

      string userInput = Console.ReadLine();

      if (userInput == ":w")
      {
        currentDocument.SaveToFile();
        documentHistory.SaveState();
        editing = false;
      }
      else if (userInput == ":q")
      {
        editing = false;
      }
      else if (userInput == ":u")
      {
        documentHistory.Undo();
      }
      else if (userInput == ":r")
      {
        documentHistory.Redo();
      }
      else
      {
        currentDocument.content += userInput + Environment.NewLine;
        documentHistory.SaveState();
      }
    }
  }
}
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

    if (CurrentDocument != null)
    {
      Console.WriteLine($"\nТекущий документ: {CurrentDocument.FileName}");
    }

    Console.Write("\nВыберите действие: ");
  }

  public void OpenDocument()
  {
    Console.Write("Введите путь к файлу: ");
    string filePath;
    filePath = Console.ReadLine();

    if (File.Exists(filePath))
    {
      CurrentDocument = new TextDocument(filePath);
      DocumentHistory = new DocumentHistory(CurrentDocument);
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
    string filePath;
    filePath = Console.ReadLine();

    CurrentDocument = new TextDocument(filePath)
    {
      Content = string.Empty
    };

    DocumentHistory = new DocumentHistory(CurrentDocument);
    Console.WriteLine("Новый документ создан.");
    Console.WriteLine("Нажмите любую клавишу...");
    Console.ReadKey();
  }

  public void EditDocument()
  {
    if (CurrentDocument == null)
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
      Console.WriteLine(CurrentDocument.Content);
      Console.WriteLine(new string('-', fiftyPercentValue));
      Console.WriteLine(":w - сохранить и выйти");
      Console.WriteLine(":q - выйти без сохранения");
      Console.WriteLine(":u - отменить");
      Console.WriteLine(":r - повторить");

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
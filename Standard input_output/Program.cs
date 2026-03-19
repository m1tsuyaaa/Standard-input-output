using System;

class Program
{
  static void Main(string[] args)
  {
    while (true)
    { 
      Console.Clear();
      Console.WriteLine("=== Главное меню ===");
      Console.WriteLine("1. Текстовый редактор");
      Console.WriteLine("2. Индексатор файлов");
      Console.WriteLine("3. Выход");
      Console.Write("Выберите приложение: ");

      string userChoice;
      userChoice = Console.ReadLine();

      if (userChoice == "1")
      {
        ConsoleTextEditor textEditor = new ConsoleTextEditor();
        textEditor.Run();
      }
      else if (userChoice == "2")
      {
        FileIndexerApplication fileIndexer = new FileIndexerApplication();
        fileIndexer.Run();
      }
      else if (userChoice == "3")
      {
        return;
      }
      else
      {
        Console.WriteLine("Неверный выбор.");
        Console.ReadKey();
      }
    }
  }
}
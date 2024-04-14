using DatabaseLibrary;
using DatabaseLibrary.ConsoleEngine;
using Spectre.Console;

namespace Flashcards.BBualdo;

internal class AppEngine
{
  public bool IsRunning { get; set; }
  public DbContext DbContext { get; set; }

  public AppEngine()
  {
    IsRunning = true;
    DbContext = new DbContext();
  }

  public void MainMenu()
  {
    AnsiConsole.Clear();

    AnsiConsole.Write(new Rule("FlashcardsApp"));

    string userChoice = ConsoleEngine.MenuSelector("What you want to do?", ["Manage Flashcards", "Study Sessions", "Quit"]);

    switch (userChoice)
    {
      case "Quit":
        AnsiConsole.Markup("[blue]Goodbye![/]\n");
        IsRunning = false;
        break;
      case "Manage Flashcards":
        // FlashcardsMenu()
        break;
      case "Study Sessions":
        // StudySessionsMenu()
        break;
    }
  }
}

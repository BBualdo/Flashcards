using DatabaseLibrary;
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
    ConsoleEngine.ShowAppTitle();
    ConsoleEngine.ShowMenuTitle("Main Menu");

    string userChoice = ConsoleEngine.MenuSelector("What you want to do?", ["Manage Flashcards", "Study Sessions", "Quit"]);

    switch (userChoice)
    {
      case "Quit":
        AnsiConsole.Markup("[blue]Goodbye![/]\n");
        IsRunning = false;
        break;
      case "Manage Flashcards":
        FlashcardsMenu();
        break;
      case "Study Sessions":
        // StudySessionsMenu()
        break;
    }
  }

  public void FlashcardsMenu()
  {
    AnsiConsole.Clear();
    ConsoleEngine.ShowAppTitle();
    ConsoleEngine.ShowMenuTitle("Flashcards Menu");

    string userChoice = ConsoleEngine.MenuSelector("", ["Back", "Manage Stacks", "Show Flashcards", "Create Flashcard", "Update Flashcard", "Delete Flashcard"]);

    switch (userChoice)
    {
      case "Back":
        MainMenu();
        break;
      case "Manage Stacks":
        StacksMenu();
        break;
      case "Show Flashcards":
        // GetAllStacks();
        // UserInput.GetStackID();
        // GetAllFlashcards();
        break;
      case "Create Flashcard":
        // GetAllStacks();
        // UserInput.GetStackID();
        // UserInput.GetQuestion();
        // UserInput.GetAnswer();
        // InsertFlashcard();
        break;
      case "Update Flashcard":
        // GetAllStacks();
        // UserInput.GetStackID();
        // GetAllFlashcards();
        // UserInput.GetFlashcardID();
        // UserInput.GetQuestion();
        // UserInput.GetAnswer();
        // UpdateFlashcard();
        break;
      case "Delete Flashcard":
        // GetAllStacks();
        // UserInput.GetStackID();
        // GetAllFlashcards();
        // UserInput.GetFlashcardID();
        // DeleteFlashcard();
        break;
    }
  }

  public void StacksMenu()
  {
    AnsiConsole.Clear();
    ConsoleEngine.ShowAppTitle();
    ConsoleEngine.ShowMenuTitle("Stacks Menu");

    string userChoice = ConsoleEngine.MenuSelector("", ["Back", "Show Stacks", "Create Stack", "Update Stack", "Delete Stack"]);

    switch (userChoice)
    {
      case "Back":
        FlashcardsMenu();
        break;
      case "Show Stacks":
        DbContext.StacksAccess.GetAllStacks();
        AnsiConsole.Markup("\n\n[blue]Press any key to return to Main Menu.[/]");
        Console.ReadKey();
        break;
      case "Create Stack":
        string stackName = UserInput.GetStackName();
        DbContext.StacksAccess.InsertStack(stackName);
        AnsiConsole.Markup("\n\n[blue]Press any key to return to Main Menu.[/]");
        Console.ReadKey();
        break;
      case "Update Stack":
        // GetAllStacks();
        // UserInput.GetStackID();
        // UserInput.GetStackName();
        // UpdateStack();
        break;
      case "Delete Stack":
        // GetAllStacks();
        // UserInput.GetStackID();
        // DeleteStack();
        break;
    }
  }
}

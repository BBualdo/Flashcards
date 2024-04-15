using DatabaseLibrary;
using DatabaseLibrary.Models;
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
        List<Stack> stacks = DbContext.StacksAccess.GetStacksList();
        DbContext.StacksAccess.GetAllStacks(stacks);
        int? id = UserInput.GetStackId(stacks);
        if (id == null) { FlashcardsMenu(); break; }

        List<FlashcardDTO> flashcards = DbContext.FlashcardsAccess.GetFlashcardsList(id);
        DbContext.FlashcardsAccess.GetAllFlashcards(flashcards);

        AnsiConsole.Markup("\n\n[blue]Press any key to return to Main Menu.[/]");
        Console.ReadKey();
        break;
      case "Create Flashcard":
        List<Stack> stacksWhereInsert = DbContext.StacksAccess.GetStacksList();
        DbContext.StacksAccess.GetAllStacks(stacksWhereInsert);
        int? idOfStackWhereInsert = UserInput.GetStackId(stacksWhereInsert);
        if (idOfStackWhereInsert == null) { FlashcardsMenu(); break; }
        string? question = UserInput.GetQuestion();
        if (question == null) { FlashcardsMenu(); break; }
        string? answer = UserInput.GetAnswer();
        if (answer == null) { FlashcardsMenu(); break; }

        DbContext.FlashcardsAccess.InsertFlashcard(idOfStackWhereInsert, question, answer);

        AnsiConsole.Markup("\n\n[blue]Press any key to return to Main Menu.[/]");
        Console.ReadKey();
        break;
      case "Update Flashcard":
        List<Stack> stacksWhereUpdate = DbContext.StacksAccess.GetStacksList();
        DbContext.StacksAccess.GetAllStacks(stacksWhereUpdate);

        int? idOfStackWhereUpdate = UserInput.GetStackId(stacksWhereUpdate);
        if (idOfStackWhereUpdate == null) { FlashcardsMenu(); break; }

        List<FlashcardDTO> flashcardsToUpdate = DbContext.FlashcardsAccess.GetFlashcardsList(idOfStackWhereUpdate);
        DbContext.FlashcardsAccess.GetAllFlashcards(flashcardsToUpdate);

        int? flashcardIdToUpdate = UserInput.GetFlashcardId(flashcardsToUpdate);
        if (flashcardIdToUpdate == null) { FlashcardsMenu(); break; }

        int? newStackId = UserInput.GetNewStackIdForFlashcard(stacksWhereUpdate);
        if (newStackId == null) { FlashcardsMenu(); break; }
        string? updatedQuestion = UserInput.GetQuestion();
        if (updatedQuestion == null) { FlashcardsMenu(); break; }
        string? updatedAnswer = UserInput.GetAnswer();
        if (updatedAnswer == null) { FlashcardsMenu(); break; }

        DbContext.FlashcardsAccess.UpdateFlashcard(flashcardIdToUpdate, newStackId, updatedQuestion, updatedAnswer);

        AnsiConsole.Markup("\n\n[blue]Press any key to return to Main Menu.[/]");
        Console.ReadKey();
        break;
      case "Delete Flashcard":
        List<Stack> stacksWhereDelete = DbContext.StacksAccess.GetStacksList();
        DbContext.StacksAccess.GetAllStacks(stacksWhereDelete);

        int? stackIdWhereDelete = UserInput.GetStackId(stacksWhereDelete);
        if (stackIdWhereDelete == null) { FlashcardsMenu(); break; }

        List<FlashcardDTO> flashcardsToDelete = DbContext.FlashcardsAccess.GetFlashcardsList(stackIdWhereDelete);
        DbContext.FlashcardsAccess.GetAllFlashcards(flashcardsToDelete);

        int? flashcardIdToDelete = UserInput.GetFlashcardId(flashcardsToDelete);
        if (flashcardIdToDelete == null) { FlashcardsMenu(); break; }

        DbContext.FlashcardsAccess.DeleteFlashcard(flashcardIdToDelete);

        AnsiConsole.Markup("\n\n[blue]Press any key to return to Main Menu.[/]");
        Console.ReadKey();
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
        List<Stack> stacksToShow = DbContext.StacksAccess.GetStacksList();
        DbContext.StacksAccess.GetAllStacks(stacksToShow);
        AnsiConsole.Markup("\n\n[blue]Press any key to return to Main Menu.[/]");
        Console.ReadKey();
        break;
      case "Create Stack":
        string? stackNameToAdd = UserInput.GetStackName();
        if (stackNameToAdd == null) { StacksMenu(); break; }

        DbContext.StacksAccess.InsertStack(stackNameToAdd);
        AnsiConsole.Markup("\n\n[blue]Press any key to return to Main Menu.[/]");
        Console.ReadKey();
        break;
      case "Update Stack":
        List<Stack> stacksToUpdate = DbContext.StacksAccess.GetStacksList();
        DbContext.StacksAccess.GetAllStacks(stacksToUpdate);
        int? stackIdToUpdate = UserInput.GetStackId(stacksToUpdate);
        if (stackIdToUpdate == null) { StacksMenu(); break; }

        string? updatedName = UserInput.GetStackName();
        if (updatedName == null) { StacksMenu(); break; }

        DbContext.StacksAccess.UpdateStack(stackIdToUpdate, updatedName);
        AnsiConsole.Markup("\n\n[blue]Press any key to return to Main Menu.[/]");
        Console.ReadKey();
        break;
      case "Delete Stack":
        List<Stack> stacksToDelete = DbContext.StacksAccess.GetStacksList();
        DbContext.StacksAccess.GetAllStacks(stacksToDelete);
        int? stackIdToDelete = UserInput.GetStackId(stacksToDelete);
        if (stackIdToDelete == null) { StacksMenu(); break; }

        DbContext.StacksAccess.DeleteStack(stackIdToDelete);
        AnsiConsole.Markup("\n\n[blue]Press any key to return to Main Menu.[/]");
        Console.ReadKey();
        break;
    }
  }
}

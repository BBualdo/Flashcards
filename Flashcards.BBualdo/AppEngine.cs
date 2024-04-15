﻿using DatabaseLibrary;
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
        ShowFlashcards();
        break;
      case "Create Flashcard":
        CreateFlashcard();
        break;
      case "Update Flashcard":
        UpdateFlashcard();
        break;
      case "Delete Flashcard":
        DeleteFlashcard();
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
        ShowStacks();
        break;
      case "Create Stack":
        CreateStack();
        break;
      case "Update Stack":
        UpdateStack();
        break;
      case "Delete Stack":
        DeleteStack();
        break;
    }
  }

  #region Stacks Methods
  private void ShowStacks()
  {
    List<Stack> stacks = DbContext.StacksAccess.GetStacksList();
    DbContext.StacksAccess.GetAllStacks(stacks);

    PressAnyKey();
  }

  private void CreateStack()
  {
    List<Stack> stacks = DbContext.StacksAccess.GetStacksList();
    string? stackName = UserInput.GetStackName(stacks);
    if (stackName == null) { StacksMenu(); return; }

    DbContext.StacksAccess.InsertStack(stackName);

    PressAnyKey();
  }

  private void UpdateStack()
  {
    List<Stack> stacks = DbContext.StacksAccess.GetStacksList();
    DbContext.StacksAccess.GetAllStacks(stacks);
    int? stackId = UserInput.GetStackId(stacks);
    if (stackId == null) { StacksMenu(); return; }

    string? name = UserInput.GetStackName(stacks);
    if (name == null) { StacksMenu(); return; }

    DbContext.StacksAccess.UpdateStack(stackId, name);

    PressAnyKey();
  }

  private void DeleteStack()
  {
    List<Stack> stacks = DbContext.StacksAccess.GetStacksList();
    DbContext.StacksAccess.GetAllStacks(stacks);

    int? stackId = UserInput.GetStackId(stacks);
    if (stackId == null) { StacksMenu(); return; }

    DbContext.StacksAccess.DeleteStack(stackId);

    PressAnyKey();
  }
  #endregion

  #region FlashcardsMethods
  private void ShowFlashcards()
  {
    List<Stack> stacks = DbContext.StacksAccess.GetStacksList();
    DbContext.StacksAccess.GetAllStacks(stacks);
    int? stackId = UserInput.GetStackId(stacks);
    if (stackId == null) { FlashcardsMenu(); return; }

    List<FlashcardDTO> flashcards = DbContext.FlashcardsAccess.GetFlashcardsList(stackId);
    DbContext.FlashcardsAccess.GetAllFlashcards(flashcards);

    PressAnyKey();
  }

  private void CreateFlashcard()
  {
    List<Stack> stacks = DbContext.StacksAccess.GetStacksList();
    DbContext.StacksAccess.GetAllStacks(stacks);

    int? stackId = UserInput.GetStackId(stacks);
    if (stackId == null) { FlashcardsMenu(); return; }

    string? question = UserInput.GetQuestion();
    if (question == null) { FlashcardsMenu(); return; }

    string? answer = UserInput.GetAnswer();
    if (answer == null) { FlashcardsMenu(); return; }

    DbContext.FlashcardsAccess.InsertFlashcard(stackId, question, answer);

    PressAnyKey();
  }

  private void UpdateFlashcard()
  {
    List<Stack> stacks = DbContext.StacksAccess.GetStacksList();
    DbContext.StacksAccess.GetAllStacks(stacks);

    int? stackId = UserInput.GetStackId(stacks);
    if (stackId == null) { FlashcardsMenu(); return; }

    List<FlashcardDTO> flashcards = DbContext.FlashcardsAccess.GetFlashcardsList(stackId);
    DbContext.FlashcardsAccess.GetAllFlashcards(flashcards);

    int? flashcardId = UserInput.GetFlashcardId(flashcards);
    if (flashcardId == null) { FlashcardsMenu(); return; }

    int? newStackId = UserInput.GetNewStackIdForFlashcard(stacks);
    if (newStackId == null) { FlashcardsMenu(); return; }

    string? question = UserInput.GetQuestion();
    if (question == null) { FlashcardsMenu(); return; }

    string? answer = UserInput.GetAnswer();
    if (answer == null) { FlashcardsMenu(); return; }

    DbContext.FlashcardsAccess.UpdateFlashcard(flashcardId, newStackId, question, answer);

    PressAnyKey();
  }

  private void DeleteFlashcard()
  {
    List<Stack> stacks = DbContext.StacksAccess.GetStacksList();
    DbContext.StacksAccess.GetAllStacks(stacks);

    int? stackId = UserInput.GetStackId(stacks);
    if (stackId == null) { FlashcardsMenu(); return; }

    List<FlashcardDTO> flashcards = DbContext.FlashcardsAccess.GetFlashcardsList(stackId);
    DbContext.FlashcardsAccess.GetAllFlashcards(flashcards);

    int? flashcardId = UserInput.GetFlashcardId(flashcards);
    if (flashcardId == null) { FlashcardsMenu(); return; }

    DbContext.FlashcardsAccess.DeleteFlashcard(flashcardId);

    PressAnyKey();
  }
  #endregion

  private void PressAnyKey()
  {
    AnsiConsole.Markup("\n\n[blue]Press any key to return to Main Menu.[/]");
    Console.ReadKey();
  }
}

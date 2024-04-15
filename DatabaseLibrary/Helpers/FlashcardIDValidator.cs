using DatabaseLibrary.Models;
using Spectre.Console;

namespace DatabaseLibrary.Helpers;

public class FlashcardIDValidator
{
  public static bool IsValid(List<FlashcardDTO> flashcards, int flashcardId)
  {
    if (flashcards.FirstOrDefault(flashcard => flashcard.Flashcard_Id == flashcardId) == null)
    {
      AnsiConsole.Markup("[red]There is no flashcard with given ID. [/]");
      return false;
    }

    return true;
  }
}

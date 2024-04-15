using Dapper;
using DatabaseLibrary.Models;
using Microsoft.Data.SqlClient;
using Spectre.Console;

namespace DatabaseLibrary;

public class FlashcardsDataAccess
{
  private string _connectionString { get; set; }

  public FlashcardsDataAccess(string connectionString)
  {
    _connectionString = connectionString;
  }

  public List<FlashcardDTO> GetFlashcardsList(int? id)
  {
    using SqlConnection connection = new SqlConnection(_connectionString);

    string sql = $@"SELECT f.flashcard_id, f.question, f.answer, s.name AS stack_name 
                    FROM flashcards f
                    JOIN stacks s ON f.stack_id=s.stack_id
                    WHERE f.stack_id={id}";

    List<FlashcardDTO> flashcards = connection.Query<FlashcardDTO>(sql).ToList();

    return flashcards;
  }

  public bool GetAllFlashcards(List<FlashcardDTO> flashcards)
  {
    if (flashcards.Count == 0)
    {
      AnsiConsole.Markup("[red]Flashcards list is empty.[/] Create one first.");
      return false;
    }

    flashcards = flashcards.OrderBy(flashcard => flashcard.Flashcard_Id).ToList();

    ConsoleEngine.ShowFlashcardsTable(flashcards);
    return true;
  }

  public bool InsertFlashcard(int? idOfStack, string? question, string? answer)
  {
    using SqlConnection connection = new SqlConnection(_connectionString);

    string sql = $"INSERT INTO flashcards(question, answer, stack_id) VALUES('{question}', '{answer}', {idOfStack})";

    int rowsAffected = connection.Execute(sql);

    if (rowsAffected == 0)
    {
      AnsiConsole.Markup("[red]Inserting Failed![/]");
      return false;
    }

    AnsiConsole.Markup($"[green]Flashcard created successfully![/]");
    return true;
  }
}

using Dapper;
using DatabaseLibrary.Models;
using Microsoft.Data.SqlClient;
using Spectre.Console;

namespace DatabaseLibrary;

public class StacksDataAccess(string connectionString)
{
  private string _connectionString = connectionString;

  public bool DeleteStack(int? id)
  {
    using SqlConnection connection = new SqlConnection(_connectionString);

    string sql = $"DELETE FROM stacks WHERE stack_id={id}";

    int affectedRows = connection.Execute(sql);

    if (affectedRows == 0)
    {
      AnsiConsole.Markup("[red]Deleting Failed![/]");
      return false;
    }

    AnsiConsole.Markup("[green]Stack deleted successfully![/]");
    return true;
  }

  public bool UpdateStack(int? id, string? updatedName)
  {
    using SqlConnection connection = new SqlConnection(_connectionString);

    string sql = $"UPDATE stacks SET name='{updatedName}' WHERE stack_id={id}";

    int affectedRows = connection.Execute(sql);

    if (affectedRows == 0)
    {
      AnsiConsole.Markup("[red]Updating Failed![/]");
      return false;
    }

    AnsiConsole.Markup("[green]Stack updated successfully![/]");
    return true;
  }

  public bool GetAllStacks(List<Stack> stacks)
  {
    if (stacks.Count == 0)
    {
      AnsiConsole.Markup("[red]Stacks list is empty.[/] Create one first.");
      return false;
    }

    stacks = stacks.OrderBy(stack => stack.Stack_Id).ToList();

    ConsoleEngine.ShowStacksTable(stacks);
    return true;
  }

  public bool InsertStack(string? stackName)
  {
    using SqlConnection connection = new(_connectionString);

    string sql = $"INSERT INTO stacks(name) VALUES('{stackName}')";

    int rowsAffected = connection.Execute(sql);

    if (rowsAffected == 0)
    {
      AnsiConsole.Markup("[red]Inserting Failed![/]");
      return false;
    }

    AnsiConsole.Markup($"[green]Created stack '{stackName}' successfully![/]");
    return true;
  }

  public List<Stack> GetStacksList()
  {
    using SqlConnection connection = new(_connectionString);

    string sql = "SELECT * FROM stacks";

    List<Stack> stacks = connection.Query<Stack>(sql).ToList();

    return stacks;
  }
}

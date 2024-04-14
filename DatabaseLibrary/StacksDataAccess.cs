using Dapper;
using DatabaseLibrary.Models;
using Microsoft.Data.SqlClient;
using Spectre.Console;

namespace DatabaseLibrary;

public class StacksDataAccess(string connectionString)
{
  private string _connectionString = connectionString;

  public bool GetAllStacks()
  {
    List<Stack> stacks = GetStacksList();

    if (stacks.Count == 0)
    {
      AnsiConsole.Markup("[red]Stacks list is empty.[/] Create one first.");
      return false;
    }

    stacks = stacks.OrderBy(stack => stack.Stack_Id).ToList();

    ConsoleEngine.ShowStacksTable(stacks);
    return true;
  }

  public bool InsertStack(string stackName)
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

  private List<Stack> GetStacksList()
  {
    using SqlConnection connection = new(_connectionString);
    connection.Open();

    string sql = "SELECT * FROM stacks";

    List<Stack> stacks = connection.Query<Stack>(sql).ToList();

    return stacks;
  }
}

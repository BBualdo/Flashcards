using Microsoft.Data.SqlClient;
using Spectre.Console;

namespace DatabaseLibrary;

public class DbContext
{
  public string ConnectionString { get; set; } = "Data Source=(localdb)\\C#AcademyProjects;Integrated Security=SSPI;TrustServerCertificate=True;Encrypt=false";

  public DbContext()
  {
    AnsiConsole.Markup("[blue]Loading...[/]");
    CreateDatabase();
    CreateTables();
  }

  private void CreateDatabase()
  {
    using SqlConnection connection = new(ConnectionString);
    connection.Open();

    string sql = "IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'flashcards') CREATE DATABASE flashcards";
    using SqlCommand command = new(sql, connection);
    command.ExecuteNonQuery();

    ConnectionString = "Data Source=(localdb)\\C#AcademyProjects;Initial Catalog=flashcards;Integrated Security=SSPI;TrustServerCertificate=True;Encrypt=false";
  }

  private void CreateTables()
  {
    using SqlConnection connection = new(ConnectionString);
    connection.Open();

    string sql = @"IF NOT EXISTS(SELECT * FROM sys.tables WHERE schema_id=SCHEMA_ID('dbo') AND name='stacks') CREATE TABLE stacks(
                  stack_id INT IDENTITY(1, 1) PRIMARY KEY,
                  name VARCHAR(40) UNIQUE NOT NULL);

                  IF NOT EXISTS(SELECT * FROM sys.tables WHERE schema_id=SCHEMA_ID('dbo') AND name='flashcards') CREATE TABLE flashcards(
                  flashcard_id INT IDENTITY(1, 1) PRIMARY KEY,
                  question VARCHAR(200) NOT NULL,
                  answer VARCHAR(200) NOT NULL,
                  stack_id INT REFERENCES stacks(stack_id));";
    using SqlCommand command = new SqlCommand(sql, connection);
    command.ExecuteNonQuery();
  }
}
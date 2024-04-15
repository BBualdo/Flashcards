using Microsoft.Data.SqlClient;
using Spectre.Console;
using System.Configuration;

namespace DatabaseLibrary;

public class DbContext
{
  private string _connectionString { get; set; }
  public StacksDataAccess StacksAccess { get; set; }

  public DbContext()
  {
    _connectionString = ConfigurationManager.AppSettings.Get("MasterConnectionString")!;
    AnsiConsole.Markup("[blue]Loading...[/]");

    CreateDatabase();
    CreateTables();

    StacksAccess = new(_connectionString);
  }

  private void CreateDatabase()
  {
    using SqlConnection connection = new(_connectionString);
    connection.Open();

    string sql = "IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'flashcards') CREATE DATABASE flashcards";
    using SqlCommand command = new(sql, connection);
    command.ExecuteNonQuery();

    _connectionString = ConfigurationManager.AppSettings.Get("ConnectionString")!;
  }

  public void CreateTables()
  {
    using SqlConnection connection = new(_connectionString);
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
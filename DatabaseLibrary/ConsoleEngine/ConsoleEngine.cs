using Spectre.Console;

namespace DatabaseLibrary.ConsoleEngine;

public class ConsoleEngine
{
  public static string MenuSelector(string title, string[] choices)
  {
    SelectionPrompt<string> prompt = new SelectionPrompt<string>()
                                    .Title(title)
                                    .AddChoices(choices);

    string choice = AnsiConsole.Prompt(prompt);

    return choice;
  }
}

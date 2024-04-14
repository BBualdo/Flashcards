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

  public static void ShowMenuTitle(string title)
  {
    Rule rule = new Rule(title).DoubleBorder().LeftJustified();
    rule.Style = new Style(Color.Blue);
    AnsiConsole.Write(rule);
  }

  public static void ShowAppTitle()
  {
    Rule rule = new Rule("FLASHCARDS").NoBorder();
    rule.Style = new Style(Color.Blue);
    AnsiConsole.Write(rule);
  }
}

using DatabaseLibrary.Helpers;
using Spectre.Console;

namespace DatabaseLibrary;

public class UserInput
{
  public static string GetStackName()
  {
    string stackName = AnsiConsole.Ask<string>("[yellow]Enter a name for stack: [/]");

    while (!StackNameValidator.IsValid(stackName))
    {
      stackName = AnsiConsole.Ask<string>("[yellow]Try again: [/]");
    }

    return stackName;
  }
}

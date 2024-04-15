using DatabaseLibrary.Helpers;
using DatabaseLibrary.Models;
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

  public static int GetStackID(List<Stack> stacks)
  {
    int stackId = AnsiConsole.Ask<int>("[yellow]Enter ID of stack you want to update: [/]");

    while (!StackIDValidator.IsValid(stacks, stackId))
    {
      stackId = AnsiConsole.Ask<int>("[yellow]Try again: [/]");
    }

    return stackId;
  }
}

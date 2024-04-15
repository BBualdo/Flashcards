using DatabaseLibrary.Helpers;
using DatabaseLibrary.Models;
using Spectre.Console;

namespace DatabaseLibrary;

public class UserInput
{
  public static string? GetStackName()
  {
    string stackName = AnsiConsole.Ask<string>("[yellow]Enter a name for stack[/] [blue]or type 0 to Stacks Menu: [/]");

    if (stackName == "0") return null;

    while (!StackNameValidator.IsValid(stackName))
    {
      stackName = AnsiConsole.Ask<string>("[yellow]Try again: [/]");
    }

    return stackName;
  }

  public static int? GetStackID(List<Stack> stacks)
  {
    int stackId = AnsiConsole.Ask<int>("[yellow]Enter ID of stack you want to update[/] [blue]or type 0 to return to Stacks Menu: [/]");

    if (stackId == 0) return null;

    while (!StackIDValidator.IsValid(stacks, stackId))
    {
      stackId = AnsiConsole.Ask<int>("[yellow]Try again: [/]");
    }

    return stackId;
  }
}

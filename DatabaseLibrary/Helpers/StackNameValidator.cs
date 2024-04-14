using Spectre.Console;

namespace DatabaseLibrary.Helpers;

public class StackNameValidator
{
  public static bool IsValid(string stackName)
  {
    if (int.TryParse(stackName, out _))
    {
      AnsiConsole.Markup("[red]Stack name can't be numeric value.[/] ");
      return false;
    }

    if (stackName.Length < 4 || stackName.Length > 40)
    {
      AnsiConsole.Markup("[red]Stack name must be between 4 and 40 characters long.[/] ");
      return false;
    }

    return true;
  }
}

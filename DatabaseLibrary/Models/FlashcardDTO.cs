namespace DatabaseLibrary.Models;

public class FlashcardDTO
{
  public int Id { get; set; }
  public string Question { get; set; }
  public string Answer { get; set; }
  public string StackName { get; set; }

  public FlashcardDTO(int id, string question, string answer, Stack stack)
  {
    Id = id;
    Question = question;
    Answer = answer;
    StackName = stack.Name;
  }
}

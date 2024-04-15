namespace DatabaseLibrary.Models;

public class Flashcard
{
  public int Flashcard_Id { get; set; }
  public string Question { get; set; }
  public string Answer { get; set; }

  public Flashcard(int id, string question, string answer)
  {
    Flashcard_Id = id;
    Question = question;
    Answer = answer;
  }
}

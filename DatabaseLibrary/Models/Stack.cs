using System.ComponentModel.DataAnnotations;

namespace DatabaseLibrary.Models;

public class Stack
{
  [Required]
  [Key]
  public int Stack_Id { get; set; }
  [Required]
  [StringLength(40)]
  public string Name { get; set; }

  public Stack() { }

  public Stack(int id, string name)
  {
    Stack_Id = id;
    Name = name;
  }
}
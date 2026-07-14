namespace Todolist2.Domain.Entities;

public class Todo
{
    public Guid Id { get; set; }

    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [System.ComponentModel.DataAnnotations.MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; set; }
}

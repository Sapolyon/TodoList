namespace Todolist2.Application.Commands;

public class UpdateTodoCommand
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}

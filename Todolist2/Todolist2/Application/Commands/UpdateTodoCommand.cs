namespace Todolist2.Application.Commands;

    public class UpdateTodoCommand
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public string Title { get; set; } = string.Empty;
        [System.ComponentModel.DataAnnotations.MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }

    }

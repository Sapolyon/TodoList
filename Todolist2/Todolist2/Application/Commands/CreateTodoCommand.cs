namespace Todolist2.Application.Commands;

    public class CreateTodoCommand
    {
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.MaxLength(500)]
        public string Description { get; set; } = string.Empty;
    }


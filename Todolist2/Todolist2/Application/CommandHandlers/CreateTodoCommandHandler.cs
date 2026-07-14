using Todolist2.Application.Commands;
using Todolist2.Domain.Entities;
using Todolist2.Infrastructure.Repositories;

namespace Todolist2.Application.CommandHandlers
{
    public class CreateTodoCommandHandler
    {
        private readonly IToDoRepository _repository;

        public CreateTodoCommandHandler(IToDoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Todo> HandleAsync(CreateTodoCommand command)
        {
            var todo = new Todo
            {
                Id = Guid.NewGuid(),
                Title = command.Title,
                Description = command.Description,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(todo);
            return todo;
        }
    }
}

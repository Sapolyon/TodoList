using TodoList.Api.Application.Commands;
using TodoList.Api.Domain.Entities;
using TodoList.Api.Infrastructure.Repositories;

namespace TodoList.Api.Application.CommandHandlers
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

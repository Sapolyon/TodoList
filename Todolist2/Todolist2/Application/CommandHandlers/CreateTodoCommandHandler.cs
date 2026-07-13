using Todolist2.Application.Commands;
using Todolist2.Domain.Entities;
using Todolist2.Infrastructure.Repositories;

namespace Todolist2.Application.CommandHandlers
{
    public class CTodoCommandHandlers
    {
        private readonly InToDoRepositories _repository;

        public CTodoCommandHandlers(InToDoRepositories repository)
        {
            _repository = repository;
        }

        public async Task Handle(CTodocommands command)
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
        }
    }
}
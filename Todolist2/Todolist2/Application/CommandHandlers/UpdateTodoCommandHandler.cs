using Todolist2.Application.Commands;
using Todolist2.Infrastructure.Repositories;

namespace Todolist2.Application.CommandHandlers
{
    public class UpdateTodoCommandHandler
    {
        private readonly IToDoRepository _repository;

        public UpdateTodoCommandHandler(IToDoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> HandleAsync(
            Guid id,
            UpdateTodoCommand command)
        {
            var todo = await _repository.GetByIdAsync(id);

            if (todo == null)
                return false;

            todo.Title = command.Title;
            todo.Description = command.Description;
            todo.IsCompleted = command.IsCompleted;

            await _repository.UpdateAsync(todo);

            return true;
        }
    }
}

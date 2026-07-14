using Todolist2.Application.Commands;
using Todolist2.Infrastructure.Repositories;

namespace Todolist2.Application.CommandHandlers
{
    public class DeleteTodoCommandHandler
    {
        private readonly IToDoRepository _repository;
        public DeleteTodoCommandHandler(IToDoRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> HandleAsync(DeleteTodoCommand command)
        {
            return await _repository.DeleteAsync(command.Id);
        }
}
}

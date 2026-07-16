using TodoList.Api.Application.Commands;
using TodoList.Api.Infrastructure.Repositories;

namespace TodoList.Api.Application.CommandHandlers
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

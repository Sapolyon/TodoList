using TodoList.Api.Domain.Entities;
using TodoList.Api.Infrastructure.Repositories;
using TodoList.Api.Application.Queries;

namespace TodoList.Api.Application.QueryHandlers
{
    public class GetTodoByIdQueryHandler
    {
        private readonly IToDoRepository _repository;
        public GetTodoByIdQueryHandler(IToDoRepository repository)
        {
            _repository = repository;
        }
        public async Task<Todo?> HandleAsync(GetTodoByIdQuery query)
        {
            return await _repository.GetByIdAsync(query.Id);

        }
    }
}

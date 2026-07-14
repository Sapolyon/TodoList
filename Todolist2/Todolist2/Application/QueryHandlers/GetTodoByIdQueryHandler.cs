using Todolist2.Domain.Entities;
using Todolist2.Infrastructure.Repositories;
using Todolist2.Application.Queries;

namespace Todolist2.Application.QueryHandlers
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

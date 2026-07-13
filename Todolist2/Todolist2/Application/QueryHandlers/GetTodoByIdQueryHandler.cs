using Todolist2.Domain.Entities;
using Todolist2.Infrastructure.Repositories;

namespace Todolist2.Application.QuarieHandlers
{
    public class GetTodoByIdquarieHandlers
    {
        private readonly IToDoRepository _repository;
        public GetTodoByIdquarieHandlers(IToDoRepository repository)
        {
            _repository = repository;
        }
        public async Task<Todo?> Handle(Guid id) 
        {
            return await _repository.GetByIdAsync(id);

        }
    }
}

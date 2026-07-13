using Todolist2.Domain.Entities;
using Todolist2.Infrastructure.Repositories;


namespace Todolist2.Application.QuarieHandlers
{
    public class GetAllTodoquarieHandlers
    {
        private readonly IToDoRepository _repository;
        public GetAllTodoquarieHandlers(IToDoRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Todo>> Handle()
        {
            return await _repository.GetAllTodosAsync();
        }
}
}

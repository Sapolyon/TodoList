using Todolist2.Domain.Entities;
using Todolist2.Infrastructure.Repositories;
using Todolist2.Application.Queries;


namespace Todolist2.Application.QueryHandlers
{
    public class GetAllTodoQueryHandler
    {
        private readonly IToDoRepository _repository;
        public GetAllTodoQueryHandler(IToDoRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Todo>> HandleAsync(GetAllTodoQuery query)
        {
            return await _repository.GetAllTodosAsync();
        }
}
}

using TodoList.Api.Domain.Entities;
using TodoList.Api.Infrastructure.Repositories;
using TodoList.Api.Application.Queries;


namespace TodoList.Api.Application.QueryHandlers
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

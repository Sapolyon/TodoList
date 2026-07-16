using TodoList.Api.Domain.Entities;

namespace TodoList.Api.Infrastructure.Repositories;

    public interface IToDoRepository
    {
        Task<List<Todo>> GetAllTodosAsync();
        Task<Todo?> GetByIdAsync(Guid id);
        Task AddAsync (Todo todo);
        Task UpdateAsync(Todo todo);
        Task<bool> DeleteAsync(Guid id);
    }


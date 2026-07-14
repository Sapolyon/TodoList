using Todolist2.Domain.Entities;

namespace Todolist2.Infrastructure.Repositories;

    public interface IToDoRepository
    {
        Task<List<Todo>> GetAllTodosAsync();
        Task<Todo?> GetByIdAsync(Guid id);
        Task AddAsync (Todo todo);
        Task UpdateAsync(Todo todo);
        Task<bool> DeleteAsync(Guid id);
    }


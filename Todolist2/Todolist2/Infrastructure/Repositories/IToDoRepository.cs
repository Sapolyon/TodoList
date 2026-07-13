using Todolist2.Domain.Entities;

namespace Todolist2.Infrastructure.Repositories;

    public interface InToDoRepositories
    {
        Task<List<Todo>> GetAllTodosAsync();
        Task<Todo?> GetByIdAsync(Guid id);
        Task AddAsync (Todo todo);
        Task UpdateAsync(Todo todo);
        Task DeleteAsync(Guid id);
    }


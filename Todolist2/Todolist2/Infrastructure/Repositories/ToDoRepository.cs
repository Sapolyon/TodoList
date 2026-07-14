using Microsoft.EntityFrameworkCore;
using Todolist2.Domain.Entities;
using Todolist2.Infrastructure.Data;

namespace Todolist2.Infrastructure.Repositories;

    public class ToDoRepository : IToDoRepository
{
        private readonly AppDbContext _context;

        public ToDoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Todo>> GetAllTodosAsync()
        {
            return await _context.Todos.ToListAsync();
        }
        public async Task<Todo?> GetByIdAsync(Guid id)
        {
            return await _context.Todos.FindAsync(id);
        }
        public async Task AddAsync(Todo todo)
        {
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Todo todo)
        {
            _context.Todos.Update(todo);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return false;
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }
    }


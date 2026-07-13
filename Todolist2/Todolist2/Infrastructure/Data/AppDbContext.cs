using Microsoft.EntityFrameworkCore;
using Todolist2.Domain.Entities;

namespace Todolist2.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using TodoManager.Application.Interfaces;
using TodoManager.Domain.Entities;
using TodoManager.Infrastructure.Persistence;

namespace TodoManager.Infrastructure
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;
        public TodoRepository(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItem?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyList<TodoItem>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.TodoItems.ToListAsync(ct);
        }

        public async Task AddAsync(TodoItem item, CancellationToken ct = default)
        {
            await _context.TodoItems.AddAsync(item, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(TodoItem item, CancellationToken ct = default)
        {
            _context.TodoItems.Update(item);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(TodoItem item, CancellationToken ct = default)
        {
            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync(ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }
    }
}

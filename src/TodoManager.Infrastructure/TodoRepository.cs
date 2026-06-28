using Microsoft.EntityFrameworkCore;
using TodoManager.Application.DTOs;
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

        public async Task<PagedResult<TodoItem>> GetPagedAsync(TodoQueryParams query, CancellationToken ct = default)
        {
            var q = _context.TodoItems.AsQueryable();

            if (query.Status is not null)
                q = q.Where(x => x.Status == query.Status);

            q = query.SortBy.ToLowerInvariant() switch
            {
                "title" => query.SortDesc
                    ? q.OrderByDescending(x => x.Title)
                    : q.OrderBy(x => x.Title),

                "duedate" => query.SortDesc
                    ? q.OrderByDescending(x => x.DueDate)
                    : q.OrderBy(x => x.DueDate),

                "status" => query.SortDesc
                    ? q.OrderByDescending(x => x.Status)
                    : q.OrderBy(x => x.Status),

                _ => query.SortDesc   // default: createdAtUtc
                    ? q.OrderByDescending(x => x.CreatedAtUtc)
                    : q.OrderBy(x => x.CreatedAtUtc)
            };
            var totalCount = await q.CountAsync(ct);

            var items = await q
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(ct);

            return new PagedResult<TodoItem> 
            {   
                Items = items,
                TotalCount = totalCount,
                Page = query.Page,
                PageSize = query.PageSize 
            };
        }


    }
}

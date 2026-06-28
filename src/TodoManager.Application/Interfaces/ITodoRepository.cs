using TodoManager.Application.DTOs;
using TodoManager.Domain.Entities;

namespace TodoManager.Application.Interfaces
{
    public interface ITodoRepository
    {
        Task<TodoItem?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<TodoItem>> GetAllAsync(CancellationToken ct = default);
        Task AddAsync(TodoItem item, CancellationToken ct = default);
        Task UpdateAsync(TodoItem item, CancellationToken ct = default);
        Task DeleteAsync(TodoItem item, CancellationToken ct = default);
        Task SaveChangesAsync(CancellationToken ct = default);
        Task<PagedResult<TodoItem>> GetPagedAsync(TodoQueryParams query, CancellationToken ct = default);


    }
}

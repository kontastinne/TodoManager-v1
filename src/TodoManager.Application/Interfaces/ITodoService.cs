using System;
using System.Collections.Generic;
using System.Text;
using TodoManager.Application.DTOs;
using TodoManager.Domain.Entities;

namespace TodoManager.Application.Interfaces
{
    public interface ITodoService
    {
        Task<IReadOnlyList<TodoItemDto>> GetAllAsync(CancellationToken ct = default);
        Task<TodoItemDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<TodoItemDto> CreateAsync(CreateTodoRequest request, CancellationToken ct = default);
        Task<TodoItemDto?> UpdateAsync(int id, UpdateTodoRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
        Task<PagedResult<TodoItemDto>> GetPagedAsync(TodoQueryParams query, CancellationToken ct = default);
    }
}

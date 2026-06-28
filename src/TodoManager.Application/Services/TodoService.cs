using TodoManager.Application.DTOs;
using TodoManager.Domain.Entities;
using TodoManager.Application.Interfaces;

namespace TodoManager.Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<TodoItemDto>> GetAllAsync(CancellationToken ct = default)
        {
            var items = await _repository.GetAllAsync(ct);
            return items.Select(ToDto).ToList();
        }

        public async Task<TodoItemDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var item = await _repository.GetByIdAsync(id, ct);
            return item != null ? ToDto(item) : null;
        }

        public async Task<TodoItemDto> CreateAsync(CreateTodoRequest request, CancellationToken ct = default)
        {
            var newItem = new TodoItem
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                Status = TodoStatus.Pending,
                CreatedAtUtc = DateTime.UtcNow,
                UpdatedAtUtc = DateTime.UtcNow
            };
            await _repository.AddAsync(newItem, ct);
            return ToDto(newItem);
        }

        public async Task<TodoItemDto?> UpdateAsync(int id, UpdateTodoRequest request, CancellationToken ct = default)
        {
            var item = await _repository.GetByIdAsync(id, ct);
            if (item == null)
                return null;

            item.Title = request.Title;
            item.Description = request.Description;
            item.DueDate = request.DueDate;
            item.Status = request.Status;
            item.UpdatedAtUtc = DateTime.UtcNow;

            await _repository.UpdateAsync(item, ct);
            return ToDto(item);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var item = await _repository.GetByIdAsync(id, ct);
            if (item == null)
                return false;

            await _repository.DeleteAsync(item, ct);
            return true;
        }

        private static TodoItemDto ToDto(TodoItem item) => new()
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            Status = item.Status,
            DueDate = item.DueDate,
            CreatedAtUtc = item.CreatedAtUtc,
            UpdatedAtUtc = item.UpdatedAtUtc
        };

        public async Task<PagedResult<TodoItemDto>> GetPagedAsync(TodoQueryParams query, CancellationToken ct = default)
        {
            var result = await _repository.GetPagedAsync(query, ct);

            return new PagedResult<TodoItemDto>
            {
                Items = result.Items.Select(ToDto).ToList(),
                TotalCount = result.TotalCount,
                Page = result.Page,
                PageSize = result.PageSize
            };
        }
    }
}



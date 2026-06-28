using Microsoft.AspNetCore.Mvc;
using TodoManager.Application.DTOs;
using TodoManager.Application.Interfaces;

namespace TodoManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodosController(ITodoService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<PagedResult<TodoItemDto>>> GetAll(
        [FromQuery] TodoQueryParams query,
        CancellationToken ct)
        {
            return Ok(await _service.GetPagedAsync(query, ct));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto?>> GetById(int id, CancellationToken ct)
        {
            var todoItem = await _service.GetByIdAsync(id, ct);
            if (todoItem is null)
                return NotFound();
            return Ok(todoItem);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> Create(CreateTodoRequest request, CancellationToken ct)
        {
            var created = await _service.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItemDto>> Update(int id, UpdateTodoRequest request, CancellationToken ct)
        {
            var updated = await _service.UpdateAsync(id, request, ct);
            if (updated is null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItemDto>> Delete(int id, CancellationToken ct)
        {
            var deleted = await _service.DeleteAsync(id, ct);
            if (!deleted)
                return NotFound();

            return NoContent();   // 204 — success, no body
        }
    }


}

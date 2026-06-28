using System;
using System.Collections.Generic;
using System.Text;
using TodoManager.Domain.Entities;

namespace TodoManager.Application.DTOs
{
    public class UpdateTodoRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TodoStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
    }
}

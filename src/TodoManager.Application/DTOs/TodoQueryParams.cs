using System;
using System.Collections.Generic;
using System.Text;
using TodoManager.Domain.Entities;

namespace TodoManager.Application.DTOs
{
    public class TodoQueryParams
    {
        public TodoStatus? Status { get; set; }
        public string SortBy { get; set; } = "createdAtUtc";
        public bool SortDesc { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

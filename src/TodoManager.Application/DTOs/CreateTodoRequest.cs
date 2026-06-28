using System;
using System.Collections.Generic;
using System.Text;

namespace TodoManager.Application.DTOs
{
   
        public class CreateTodoRequest
        {
        public string Title { get; set; } =  string.Empty;
            public string? Description { get; set; }
            public DateTime? DueDate { get; set; }
        }
}

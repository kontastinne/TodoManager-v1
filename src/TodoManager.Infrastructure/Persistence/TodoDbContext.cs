using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TodoManager.Domain.Entities;

namespace TodoManager.Infrastructure.Persistence
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options){ }
        public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    }
}

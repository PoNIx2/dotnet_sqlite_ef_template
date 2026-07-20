using dotnet_sqlite_ef_template.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_sqlite_ef_template.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
}
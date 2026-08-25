using Microsoft.EntityFrameworkCore;
using TaskPrioritizer2.Models;

namespace TaskPrioritizer2.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<StudentTask> Tasks => Set<StudentTask>();
}
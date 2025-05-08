using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> o) : base(o)
    {
    }

    public DbSet<TaskItemEntity> TaskItems { get; set; }
}
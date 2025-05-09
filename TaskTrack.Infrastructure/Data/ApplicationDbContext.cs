using Microsoft.EntityFrameworkCore;
using TaskTrack.Infrastructure.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> o) : base(o)
    {
    }

    public DbSet<TaskItemEntity> TaskItems { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<JwtRefreshTokenEntity> JwtRefreshTokens { get; set; }
}
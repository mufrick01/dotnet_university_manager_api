using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<University> Universities => Set<University>();
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
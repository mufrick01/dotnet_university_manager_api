using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<University> Universities => Set<University>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Professor> Professors => Set<Professor>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<ProfessorCourse> ProfessorCourses => Set<ProfessorCourse>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
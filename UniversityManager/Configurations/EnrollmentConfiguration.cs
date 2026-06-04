using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("Enrollments");

        // PK compuesta
        builder.HasKey(x => new
        {
            x.StudentId,
            x.CourseId
        });

        // Propiedades
        builder.Property(x => x.FinalGrade)
            .HasPrecision(3, 1)
            .IsRequired(false);

        builder.Property(x => x.EnrollmentDate)
            .IsRequired();

        // Relación Student -> Enrollment
        builder.HasOne(x => x.Student)
            .WithMany(x => x.Enrollments)
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relación Course -> Enrollment
        builder.HasOne(x => x.Course)
            .WithMany(x => x.Enrollments)
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        // Índice para la segunda FK
        builder.HasIndex(x => x.CourseId);
    }
}
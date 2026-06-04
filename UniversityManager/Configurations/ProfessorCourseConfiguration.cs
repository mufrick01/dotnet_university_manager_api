
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProfessorCourseConfiguration : IEntityTypeConfiguration<ProfessorCourse>
{
    public void Configure(EntityTypeBuilder<ProfessorCourse> builder)
    {
        builder.ToTable("ProfessorCourses");

        builder.HasKey(pc => new
        {
            pc.ProfessorId,
            pc.CourseId
        });

        builder.Property(pc => pc.Semester)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasOne(pc => pc.Professor)
            .WithMany(p => p.ProfessorCourses)
            .HasForeignKey(pc => pc.ProfessorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pc => pc.Course)
            .WithMany(c => c.ProfessorCourses)
            .HasForeignKey(pc => pc.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
    }


}
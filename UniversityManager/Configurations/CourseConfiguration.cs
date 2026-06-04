using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CourseConfiguration
    : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(20)
            .IsUnicode(false);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200)
            .IsUnicode();

        builder.Property(x => x.Credits)
            .IsRequired();

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.HasOne(x => x.Department)
            .WithMany(x => x.Courses)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_Course_Credits",
                "[Credits] >= 1 AND [Credits] <= 20");
        });
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StudentConfiguration
    : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(200)
            .IsUnicode();

        builder.Property(x => x.BirthDate)
            .IsRequired();

        builder.HasOne(x => x.University)
            .WithMany(x => x.Students)
            .HasForeignKey(x => x.UniversityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Tutor)
            .WithMany(x => x.Students)
            .HasForeignKey(x => x.TutorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_Student_BirthDate",
                "[BirthDate] >= '1900-01-01' AND [BirthDate] < GETDATE()");
        });
    }
}
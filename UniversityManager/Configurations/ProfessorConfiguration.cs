using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
{
    public void Configure(EntityTypeBuilder<Professor> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName)
            .HasMaxLength(200)
            .IsRequired()
            .IsUnicode();

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(255)
            .IsUnicode(false);

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.HireDate)
            .IsRequired();

        builder.HasOne(x => x.Department)
            .WithMany(x => x.Professors)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_Professor_HireDate",
            "[HireDate]>='1900-01-01' AND [HireDate] <= GETDATE()"
        ));

    }
}
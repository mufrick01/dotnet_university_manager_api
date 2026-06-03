using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Budget)
            .HasPrecision(18, 2);

        builder.HasOne(x => x.University)
               .WithMany(x => x.Departments)
               .HasForeignKey(x => x.UniversityId);
    }
}
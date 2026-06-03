using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UniversityConfiguration : IEntityTypeConfiguration<University>
{
    public void Configure(EntityTypeBuilder<University> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode();

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.FoundationDate)
            .IsRequired();


        builder.ToTable(t =>
            t.HasCheckConstraint(
                "CK_University_FoundationDate",
                "[FoundationDate] >= '1700-01-01' AND [FoundationDate] <= GETDATE()"
            )
        );


    }
}
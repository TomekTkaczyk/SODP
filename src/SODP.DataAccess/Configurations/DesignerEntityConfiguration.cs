using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Domain.Entities;
using SODP.Domain.ValueObjects;

namespace SODP.DataAccess.Configurations;

public class DesignerEntityConfiguration : IEntityTypeConfiguration<Designer>
{
    public void Configure(EntityTypeBuilder<Designer> builder)
    {
        builder.Property(x => x.Title)
		.HasConversion(x => x.Value, x => new DesignerTitle(x))
		.HasColumnType("nvarchar(20)");

        builder.Property(x => x.Firstname)
		.HasConversion(x => x.Value, x => new FirstName(x))
		.HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(x => x.Lastname)
		.HasConversion(x => x.Value, x => new LastName(x))
		.HasColumnType("nvarchar(50)")
            .IsRequired();

	builder.Property(x => x.ActiveStatus)
        .HasColumnType("tinyint(1)")
        .HasDefaultValue(true)
        .IsRequired();

	builder.ToTable("Designers");
    }
}

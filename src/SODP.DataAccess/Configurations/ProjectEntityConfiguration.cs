using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Domain.Entities;
using SODP.Domain.ValueObjects;

namespace SODP.DataAccess.Configurations;

public class ProjectEntityConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Ignore(p => p.Symbol);

        builder.Property(p => p.Number)
			.HasConversion(v => v.Value,v => new ProjectNumber(v))
            .HasColumnType("varchar(4)")
            .IsRequired();

        builder.Property(p => p.Name)
			.HasConversion(v => v.Value, v => new ProjectName(v))
			.HasColumnType("nvarchar(256)")
            .IsRequired();

        builder.Property(p => p.Title)
            .HasConversion(v => v.Value, v => new Title(v))
            .HasColumnType("nvarchar(256)");

        builder.Property(p => p.Address)
            .HasConversion(v => v.Value, v => new Address(v))
            .HasColumnType("nvarchar(256)");

        builder.Property(p => p.LocationUnit)
            .HasColumnType("nvarchar(256)")
            .HasDefaultValue("");

        builder.Property(p => p.BuildingCategory)
            .HasColumnType("nvarchar(256)")
            .HasDefaultValue("");

        builder.Property(p => p.Investor)
            .HasColumnType("nvarchar(256)")
            .HasDefaultValue("");

        builder.Property(p => p.BuildingPermit)
            .HasColumnType("nvarchar(256)")
            .HasDefaultValue("");

        builder.Property(p => p.Description)
            .HasDefaultValue("")
            .HasColumnType("longtext");

		builder.HasIndex(p => new { p.Number, p.StageId })
            .IsUnique()
            .HasName("ProjectsIX_NumberStage");

        builder.HasIndex(p => p.StageId)
            .HasName("ProjectsIX_Stage");

        builder.ToTable("Projects");

        builder.HasMany(x => x.Parts)
            .WithOne(y => y.Project)
            .HasForeignKey(z => z.ProjectId);
            // .HasConstraintName("FK_Project");
    }
}

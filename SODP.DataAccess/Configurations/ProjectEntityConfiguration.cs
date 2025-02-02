using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;

namespace SODP.DataAccess.Configurations
{
    public class ProjectEntityConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Ignore(p => p.Symbol);

            builder.Property(p => p.Number)
                .HasColumnType("varchar(4)")
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnType("nvarchar(256)")
                .IsRequired();

            builder.Property(p => p.Title)
                .HasColumnType("nvarchar(256)")
                .HasDefaultValue("");

            builder.Property(p => p.Address)
                .HasColumnType("nvarchar(256)")
                .HasDefaultValue("");

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
                .HasDatabaseName("ProjectsIX_NumberStage")
                .IsUnique();

            builder.HasIndex(p => p.StageId)
                .HasDatabaseName("ProjectsIX_Stage");

            builder.ToTable("Projects");

            builder.HasMany(x => x.Parts)
                .WithOne(y => y.Project)
                .HasForeignKey(z => z.ProjectId);
                // .HasConstraintName("FK_Project");
        }
    }
}

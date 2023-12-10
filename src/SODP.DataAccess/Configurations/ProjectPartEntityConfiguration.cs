using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Domain.Entities;
using SODP.Domain.ValueObjects;

namespace SODP.DataAccess.Configurations;

public class ProjectPartEntityConfiguration : IEntityTypeConfiguration<ProjectPart>
    {
        public void Configure(EntityTypeBuilder<ProjectPart> builder)
        {
            builder.Property(x => x.ProjectId)
                .IsRequired();

		builder.Property(x => x.Sign)
			.HasConversion(x => x.Value, x => new Sign(x))
			.HasColumnType("nvarchar(10)")
			.IsRequired();

		builder.Property(x => x.Title)
			.HasConversion(x => x.Value, x => new Title(x))
			.HasColumnType("nvarchar(50)")
			//.HasColumnName("Name")
			.HasColumnName("Title")
			.IsRequired();

		builder.Property(x => x.Order)
		   .HasColumnType("int")
		   .HasDefaultValue(1)
		   .IsRequired();

		builder.HasIndex(x => x.ProjectId)
                .HasName("ProjectPartsIX_Project");

            builder.ToTable("ProjectParts");

            builder.HasMany(x => x.Branches)
                .WithOne(y => y.ProjectPart)
                .HasForeignKey(z => z.ProjectPartId);
                //.HasConstraintName("FK_ProjectPart_ProjectPartId");
        }
    }

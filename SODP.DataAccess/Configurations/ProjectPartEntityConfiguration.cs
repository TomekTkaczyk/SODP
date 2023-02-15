using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.DataAccess.Configurations
{
    public class ProjectPartEntityConfiguration : IEntityTypeConfiguration<ProjectPart>
    {
        public void Configure(EntityTypeBuilder<ProjectPart> builder)
        {
            builder.Property(x => x.ProjectId)
                .IsRequired();

			builder.Property(x => x.Sign)
				.HasColumnType("nvarchar(10)")
				.IsRequired();

			builder.Property(x => x.Name)
				.HasColumnType("nvarchar(50)")
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
}

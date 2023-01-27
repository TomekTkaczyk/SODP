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

            builder.Property(x => x.PartId)
                .IsRequired();

            builder.HasIndex(x => x.ProjectId)
                .HasName("ProjectPartIX_Project");

            builder.HasIndex(x => x.PartId)
                .HasName("ProjectPartIX_Part");

            builder.ToTable("ProjectBranches");

            builder.HasMany(x => x.Branches)
                .WithOne(y => y.ProjectPart)
                .HasForeignKey(z => z.ProjectPartId);
                //.HasConstraintName("FK_ProjectBranch");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.DataAccess.Configurations
{
    public class ProjectBranchEntityConfiguration : IEntityTypeConfiguration<ProjectBranch>
    {
        public void Configure(EntityTypeBuilder<ProjectBranch> builder)
        {
            builder.Property(x => x.ProjectId)
                .IsRequired();

            builder.Property(x => x.BranchId)
                .IsRequired();

            builder.HasIndex(x => x.ProjectId)
                .HasName("ProjectBranchesIX_Project");

            builder.HasIndex(x => x.BranchId)
                .HasName("ProjectBranchesIX_Branch");

            builder.ToTable("ProjectBranches");

            builder.HasMany(x => x.Roles)
                .WithOne(y => y.ProjectBranch)
                .HasForeignKey(z => z.ProjectBranchId);
                //.HasConstraintName("FK_ProjectBranch");
        }
    }
}

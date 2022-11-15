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

            //builder.Property(x => x.DesignerLicenseId);

            //builder.Property(x => x.CheckingLicenseId);

            builder.HasIndex(x => x.ProjectId)
                .HasName("IX_Project");

            builder.HasIndex(x => x.BranchId)
                .HasName("IX_Branch");

            //builder.HasIndex(x => x.DesignerLicenseId)
            //    .HasName("IX_Designer");

            //builder.HasIndex(x => x.CheckingLicenseId)
            //    .HasName("IX_Checking");

            builder.ToTable("ProjectBranches");

            builder.HasMany(x => x.Roles)
                .WithOne(y => y.ProjectBranch)
                .HasForeignKey(z => z.ProjectBranchId)
                .HasConstraintName("FK_ProjectBranch");

            //builder.HasOne(x => x.Branch)
            //    .WithOne()
            //    .HasConstraintName("FK_Branch")
            //    .OnDelete(DeleteBehavior.Restrict);


            //builder.HasOne(x => x.Designer)
            //    .WithMany()
            //    .HasForeignKey(x => x.DesignerId)
            //    .HasConstraintName("FK_ProjectBranch_Designer")
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(x => x.Checking)
            //    .WithMany()
            //    .HasForeignKey(x => x.CheckingId)
            //    .HasConstraintName("FK_ProjectBranch_Checking")
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(x => x.Branch)
            //    .WithMany()
            //    .HasForeignKey(x => x.BranchId)
            //    .HasConstraintName("FK_ProjectBranch_Branch")
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

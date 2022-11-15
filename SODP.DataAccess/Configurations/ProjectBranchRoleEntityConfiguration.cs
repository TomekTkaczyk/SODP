using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;

namespace SODP.DataAccess.Configurations
{
    public class ProjectBranchRoleEntityConfiguration : IEntityTypeConfiguration<ProjectBranchRole>
    {
        public void Configure(EntityTypeBuilder<ProjectBranchRole> builder)
        {
            builder.Property(x => x.ProjectBranchId)
                .IsRequired();

            builder.Property(x => x.Role)
                .IsRequired();

            builder.Property(x => x.ProjectBranchId)
               .IsRequired();
            
            builder.HasIndex(x => x.LicenseId)
                .HasName("IX_Branch");

            builder.HasIndex(x => x.LicenseId)
                .HasName("IX_License");

            builder.ToTable("ProjectBranchRoles");

            builder.HasKey(x => new { x.ProjectBranchId, x.Role, x.LicenseId });

            //builder.HasOne(x => x.License)
            //    .WithMany(y => y.Branches)
            //    .HasForeignKey(x => x.LicenseId);

            //builder.HasOne(x => x.Branch)
            //    .WithMany(y => y.Licenses)
            //    .HasForeignKey(x => x.BranchId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

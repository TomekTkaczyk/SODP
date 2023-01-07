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

            builder.Property(x => x.LicenseId)
                .IsRequired();

            builder.HasIndex(x => x.LicenseId)
                .HasName("ProjectBranchRolesIX_License");

            builder.ToTable("ProjectBranchRoles");

            builder.HasKey(x => new { x.ProjectBranchId, x.Role, x.LicenseId });
        }
    }
}

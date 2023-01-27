using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;

namespace SODP.DataAccess.Configurations
{
    public class BranchRoleEntityConfiguration : IEntityTypeConfiguration<BranchRole>
    {
        public void Configure(EntityTypeBuilder<BranchRole> builder)
        {
            builder.Property(x => x.PartBranchId)
                .IsRequired();

            builder.Property(x => x.Role)
                .IsRequired();

            builder.Property(x => x.LicenseId)
                .IsRequired();

            builder.HasIndex(x => x.LicenseId)
                .HasName("BranchRolesIX_License");

            builder.ToTable("BranchRoles");

            builder.HasKey(x => new { x.PartBranchId, x.Role, x.LicenseId });
        }
    }
}

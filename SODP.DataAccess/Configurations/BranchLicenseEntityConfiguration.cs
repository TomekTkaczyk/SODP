using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;

namespace SODP.DataAccess.Configurations
{
    public class BranchLicenseEntityConfiguration : IEntityTypeConfiguration<BranchLicense>
    {
        public void Configure(EntityTypeBuilder<BranchLicense> builder)
        {
            builder.Property(x => x.BranchId)
                .IsRequired();

            builder.Property(x => x.LicenseId)
                .IsRequired();

            builder.HasIndex(x => x.BranchId)
                .HasName("BranchLicensesIX_Branch");

            builder.HasIndex(x => x.LicenseId)
                .HasName("BranchLicensesIX_License");

            builder.ToTable("BranchLicenses");

            builder.HasOne(x => x.License)
                .WithMany(y => y.Branches)
                .HasForeignKey(x => x.LicenseId);

            builder.HasOne(x => x.Branch)
                .WithMany(y => y.Licenses)
                .HasForeignKey(x => x.BranchId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

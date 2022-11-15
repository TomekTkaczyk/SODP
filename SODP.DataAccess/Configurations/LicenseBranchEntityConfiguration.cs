using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;

namespace SODP.DataAccess.Configurations
{
    public class LicenseBranchEntityConfiguration : IEntityTypeConfiguration<LicenseBranch>
    {
        public void Configure(EntityTypeBuilder<LicenseBranch> builder)
        {
            builder.Property(x => x.BranchId)
                .IsRequired();

            builder.Property(x => x.LicenseId)
                .IsRequired();

            builder.HasIndex(x => x.BranchId)
                .HasName("IX_Branch");

            builder.HasIndex(x => x.LicenseId)
                .HasName("IX_License");

            builder.ToTable("LicenseBranches");

            builder.HasKey(x => new { x.BranchId, x.LicenseId });

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

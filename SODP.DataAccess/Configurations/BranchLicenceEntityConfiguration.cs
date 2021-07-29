using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;

namespace SODP.DataAccess.Configurations
{
    public class BranchLicenceEntityConfiguration : IEntityTypeConfiguration<BranchLicence>
    {
        public void Configure(EntityTypeBuilder<BranchLicence> builder)
        {
            builder.Property(x => x.BranchId)
                .IsRequired();

            builder.Property(x => x.LicenceId)
                .IsRequired();

            builder.HasIndex(x => x.BranchId)
                .HasName("IX_Branch");

            builder.HasIndex(x => x.LicenceId)
                .HasName("IX_Licence");

            builder.ToTable("BranchLicence");
        }
    }
}

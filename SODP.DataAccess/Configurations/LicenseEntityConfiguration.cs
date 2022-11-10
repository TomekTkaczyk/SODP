using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;

namespace SODP.DataAccess.Configurations
{
    public class LicenseEntityConfiguration : IEntityTypeConfiguration<License>
    {
        public void Configure(EntityTypeBuilder<License> builder)
        {
            builder.Property(x => x.Content)
                .HasColumnType("nvarchar(250)")
                .IsRequired();

            builder.HasIndex(x => x.DesignerId)
                .HasName("IX_Designer");

            builder.ToTable("Licenses");
        }
    }
}

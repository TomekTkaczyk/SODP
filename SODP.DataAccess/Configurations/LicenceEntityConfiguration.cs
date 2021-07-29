using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;

namespace SODP.DataAccess.Configurations
{
    public class LicenceEntityConfiguration : IEntityTypeConfiguration<Licence>
    {
        public void Configure(EntityTypeBuilder<Licence> builder)
        {
            builder.Property(x => x.Contents)
                .HasColumnType("nvarchar(250)")
                .IsRequired();

            builder.HasIndex(x => x.DesignerId)
                .HasName("IX_Designer");

            builder.ToTable("Licences");
        }
    }
}

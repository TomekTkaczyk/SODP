using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Domain.Entities;

namespace SODP.DataAccess.Configurations
{
    public class CertificateEntityConfiguration : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            builder.Property(x => x.Number)
                .HasColumnType("nvarchar(20)")
                .IsRequired();

            builder.HasIndex(x => x.DesignerId)
                .HasName("CertificatesIX_Designer");

            builder.ToTable("Certificates");
        }
    }
}

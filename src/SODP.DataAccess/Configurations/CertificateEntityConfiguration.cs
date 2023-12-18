using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Domain.Entities;
using SODP.Domain.ValueObjects;

namespace SODP.DataAccess.Configurations;

public class CertificateEntityConfiguration : IEntityTypeConfiguration<Certificate>
{
    public void Configure(EntityTypeBuilder<Certificate> builder)
    {
        builder.Property(x => x.Number)
				.HasConversion(x => x.Value, x => new CertificateNumber(x))
				.HasColumnType("nvarchar(20)")
            .IsRequired();

        builder.HasIndex(x => x.DesignerId)
            .HasName("CertificatesIX_Designer");

        builder.ToTable("Certificates");
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;

namespace SODP.DataAccess.Configurations
{
    public class PartEntityConfiguration : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.Property(x => x.Order)
               .HasColumnType("int")
               .HasDefaultValue(1)
               .IsRequired();

            builder.Property(x => x.Sign)
                .HasColumnType("nvarchar(10)")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(x => x.ActiveStatus)
                .HasColumnType("tinyint(1)")
                .HasDefaultValue(true)
                .IsRequired();

			builder.HasKey(u => u.Id);

			builder.HasIndex(x => new { x.Order })
                .HasDatabaseName("PartIX_Order");

            builder.ToTable("Parts");
        }
    }
}

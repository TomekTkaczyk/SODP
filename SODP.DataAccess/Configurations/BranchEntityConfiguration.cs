using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;

namespace SODP.DataAccess.Configurations
{
    public class BranchEntityConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
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
                .HasName("BranchesIX_Order");

            builder.ToTable("Branches");
        }
    }
}

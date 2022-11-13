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
                .HasDefaultValue(1)
                .IsRequired();

            builder.HasIndex(x => new { x.Order })
                .HasName("IX_ORDER");

            builder.ToTable("Branches");

            //builder.HasMany(x => x.Licenses)
            //    .WithOne(x => x.Branch)
            //    .HasForeignKey(x => x.BranchId)
            //    .HasConstraintName("FK_Branch_License")
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

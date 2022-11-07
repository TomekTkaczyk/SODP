using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;

namespace SODP.DataAccess.Configurations
{
    public class BranchEntityConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.Property(x => x.Symbol)
               .HasColumnType("varchar(2)")
               .HasDefaultValue("00")
               .IsRequired();

            builder.Property(x => x.Sign)
                .HasColumnType("nvarchar(10)")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnType("nvarchar(50)");

            builder.Property(x => x.ActiveStatus)
                .HasDefaultValue(1)
                .IsRequired();

            builder.HasIndex(x => new { x.Symbol })
                .HasName("IX_SYMBOL")
                .IsUnique();

            builder.ToTable("Branches");

            //builder.HasMany(x => x.Licenses)
            //    .WithOne(x => x.Branch)
            //    .HasForeignKey(x => x.BranchId)
            //    .HasConstraintName("FK_Branch_License")
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

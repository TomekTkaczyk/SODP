using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;

namespace SODP.DataAccess.Configurations
{
    public class BranchEntityConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.Property(x => x.Sign)
                .HasColumnType("varchar(10)")
                .IsRequired();

            builder.Property(x => x.Title)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.ToTable("Branches");

            //builder.HasMany(x => x.Licenses)
            //    .WithOne(x => x.Branch)
            //    .HasForeignKey(x => x.BranchId)
            //    .HasConstraintName("FK_Branch_Licence")
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

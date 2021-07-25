using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;

namespace SODP.DataAccess.Configurations
{
    public class BranchDesignerEntityConfiguration : IEntityTypeConfiguration<BranchDesigner>
    {
        public void Configure(EntityTypeBuilder<BranchDesigner> builder)
        {
            builder.Property(x => x.BranchId)
                .IsRequired();

            builder.Property(x => x.DesignerId)
                .IsRequired();

            builder.HasIndex(x => x.BranchId)
                .HasName("IX_Branch");

            builder.HasIndex(x => x.DesignerId)
                .HasName("IX_Designer");

            builder.ToTable("BranchDesigners");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;

namespace SODP.DataAccess.Configurations
{
    public class DesignerEntityConfiguration : IEntityTypeConfiguration<Designer>
    {
        public void Configure(EntityTypeBuilder<Designer> builder)
        {
            builder.Property(x => x.Title)
                .HasColumnType("nvarchar(20)")
                .IsRequired();

            builder.Property(x => x.Firstname)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(x => x.Lastname)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.ToTable("Designers");

            //builder.HasMany(x => x.Certificates)
            //    .WithOne(x => x.Designer)
            //    .HasForeignKey(x => x.DesignerId)
            //    .HasConstraintName("FK_Designer_Certificate")
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.HasMany(x => x.Licenses)
            //    .WithOne(x => x.Designer)
            //    .HasForeignKey(x => x.DesignerId)
            //    .HasConstraintName("FK_Designer_License")
            //    .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

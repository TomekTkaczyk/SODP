using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Domain.Entities;

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

            //         builder.OwnsOne(x => x.Differentiator, d =>
            //{                 
            //             d.Property(x => x.Sign)
            //	 .HasColumnType("nvarchar(10)")
            //	 .HasColumnName("Sign")
            //	 .IsRequired();
            //             d.Property(x => x.Title)
            //	 .HasColumnType("nvarchar(50)")
            //	 .HasColumnName("Name")
            //	 .IsRequired();
            //});

            builder.Property(x => x.Sign)
                .HasColumnType("nvarchar(10)")
                .HasColumnName("Sign")
                .IsRequired();

            builder.Property(x => x.Title)
                .HasColumnType("nvarchar(50)")
                .HasColumnName("Name")
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

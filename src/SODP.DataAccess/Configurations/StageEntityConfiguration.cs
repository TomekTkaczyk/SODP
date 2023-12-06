using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Domain.Entities;
using SODP.Domain.ValueObjects;

namespace SODP.DataAccess.Configurations
{
    public class StageEntityConfiguration : IEntityTypeConfiguration<Stage>
    {
        public void Configure(EntityTypeBuilder<Stage> builder)
        {
			builder.Property(x => x.Order)
               .HasColumnType("int")
               .HasDefaultValue(1)
               .IsRequired();

			builder.Property(x => x.Sign)
				.HasConversion(x => x.Value, x => new Sign(x))
				.HasColumnType("nvarchar(10)")
				.HasColumnName("Sign")
				.IsRequired();

			builder.Property(x => x.Title)
				.HasConversion(x => x.Value, x => new Title(x))
				.HasColumnType("nvarchar(50)")
				.HasColumnName("Name")
				//.HasColumnName("Title")
				.IsRequired();

			builder.Property(x => x.ActiveStatus)
                .HasColumnType("tinyint(1)")
                .HasDefaultValue(true)
                .IsRequired();

			builder.HasKey(u => u.Id);

			//builder.HasIndex(x => new { x.Order })
			//	 .HasName("PartIX_Order");

			builder.ToTable("Stages");
        }
    }
}

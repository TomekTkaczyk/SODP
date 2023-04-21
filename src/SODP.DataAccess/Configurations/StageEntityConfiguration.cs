using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Domain.Entities;

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

			builder.ToTable("Stages");
        }
    }
}

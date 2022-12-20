using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;

namespace SODP.DataAccess.Configurations
{
    public class StageEntityConfiguration : IEntityTypeConfiguration<Stage>
    {
        public void Configure(EntityTypeBuilder<Stage> builder)
        {
            builder.Property(p => p.Sign)
                .HasColumnType("nvarchar(10)")
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnType("nvarchar(50)")
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

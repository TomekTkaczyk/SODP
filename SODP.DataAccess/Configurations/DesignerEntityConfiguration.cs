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
                .HasDefaultValue("");

            builder.Property(x => x.Firstname)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(x => x.Lastname)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

			builder.Property(x => x.ActiveStatus)
	            .HasColumnType("tinyint(1)")
	            .HasDefaultValue(true)
	            .IsRequired();

			builder.ToTable("Designers");
        }
    }
}

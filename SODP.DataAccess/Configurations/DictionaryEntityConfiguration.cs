using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;

namespace SODP.DataAccess.Configurations
{
	public class DictionaryEntityConfiguration : IEntityTypeConfiguration<AppDictionary>
	{
		public void Configure(EntityTypeBuilder<AppDictionary> builder)
		{
			builder.Property(x => x.Master)
				.HasColumnType("nvarchar(10)")
				.HasDefaultValue("");
			
			builder.Property(x => x.Sign)
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

			builder.ToTable("Dictionary");
		}
	}
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODP.DataAccess.Configurations
{
	public class AppDictionaryEntityConfiguration : IEntityTypeConfiguration<AppDictionary>
	{
		public void Configure(EntityTypeBuilder<AppDictionary> builder)
		{
			builder.Property(x => x.Sign)
				.HasColumnType("nvarchar(10)")
				.IsRequired();

			builder.Property(p => p.Name)
				.HasColumnType("nvarchar(50)")
				.HasDefaultValue("Cos tam costam")
                .IsRequired();

			builder.Property(x => x.ActiveStatus)
				.HasColumnType("tinyint(1)")
				//.HasDefaultValue(true)
				.IsRequired();

			builder.HasKey(u => u.Id);

			builder.ToTable("Dictionary");
		}
	}
}

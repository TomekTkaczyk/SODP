using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;

namespace SODP.DataAccess.Configurations
{
	internal class InvestorEntityConfiguration : IEntityTypeConfiguration<Investor>
	{
		public void Configure(EntityTypeBuilder<Investor> builder)
		{
			builder.Property(x => x.Name)
				.HasColumnType("nvarchar(256)")
				.IsRequired();

			builder.Property(x => x.ActiveStatus)
				.HasColumnType("tinyint(1)")
				.HasDefaultValue(true)
				.IsRequired();
			
			builder.HasKey(u => u.Id);

			builder.HasIndex(x => new { x.Name })
				.HasDatabaseName("InvestorsIX_Name");

			builder.ToTable("Investors");
		}
	}
}

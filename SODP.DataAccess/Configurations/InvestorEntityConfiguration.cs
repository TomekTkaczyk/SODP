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

			builder.HasIndex(x => new { x.Name })
				.HasName("IX_NAME");

			builder.ToTable("Investors");
		}
	}
}

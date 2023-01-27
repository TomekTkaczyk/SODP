using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;

namespace SODP.DataAccess.Configurations
{
    public class PartBranchEntityConfiguration : IEntityTypeConfiguration<PartBranch>
    {
        public void Configure(EntityTypeBuilder<PartBranch> builder)
        {
            builder.Property(x => x.ProjectPartId)
                .IsRequired();

			builder.HasIndex(x => new { x.ProjectPartId })
            	.HasName("PartBranchesIX_ProjectPartId");


			builder.ToTable("PartBranches");
        }
    }
}

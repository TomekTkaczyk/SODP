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

            builder.ToTable("PartBranches");
        }
    }
}

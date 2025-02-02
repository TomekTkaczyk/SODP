using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;

namespace SODP.DataAccess.Configurations
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(r => r.ConcurrencyStamp)
                .IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.Name)
                .HasColumnType("nvarchar(256)")
                .IsRequired();

            builder.Property(u => u.NormalizedName)
                .HasColumnType("nvarchar(256)")
                .IsRequired();

            // Index for "normalized" role name to allow efficient lookups
            builder.HasIndex(r => r.Name)
                .HasDatabaseName("RolesIX_Name")
                .IsUnique();

            builder.HasIndex(r => r.NormalizedName)
                .HasDatabaseName("RolesIX_NormalizedName")
                .IsUnique();

            builder.ToTable("Roles");
        }
    }
}

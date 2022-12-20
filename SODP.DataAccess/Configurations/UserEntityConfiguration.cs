using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;

namespace SODP.DataAccess.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(u => u.ConcurrencyStamp)
                .IsConcurrencyToken();

            builder.Property(u => u.UserName)
                .HasColumnType("nvarchar(256)")
                .IsRequired();

            builder.Property(u => u.NormalizedUserName)
                .HasColumnType("nvarchar(256)")
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnType("nvarchar(256)");

            builder.Property(u => u.NormalizedEmail)
                .HasColumnType("nvarchar(256)");

            builder.Property(u => u.Firstname)
                .HasDefaultValue("")
                .HasColumnType("nvarchar(256)");

            builder.Property(u => u.Lastname)
               .HasDefaultValue("")
               .HasColumnType("nvarchar(256)");

            builder.Property(u => u.PhoneNumber)
                .HasColumnType("varchar(256)");

            builder.Property(u => u.ConcurrencyStamp)
                .HasColumnType("varchar(256)")
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();

            builder.Property(u => u.PasswordHash)
                .HasColumnType("varchar(256)");

            builder.Property(u => u.SecurityStamp)
                .HasColumnType("varchar(256)");

			builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.UserName)
                .HasName("IX_UserName")
                .IsUnique();

            builder.HasIndex(u => u.NormalizedUserName)
                .HasName("IX_MormalizedUserName")
                .IsUnique();

            builder.HasIndex(u => u.Email)
                .HasName("IX_Email");

            builder.HasIndex(u => u.NormalizedEmail)
                .HasName("IX_NormalizedEmail");


            builder.ToTable("Users");
        }
    }
}

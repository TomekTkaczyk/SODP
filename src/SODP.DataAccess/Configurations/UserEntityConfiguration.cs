using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Domain.Entities;
using SODP.Domain.ValueObjects;

namespace SODP.DataAccess.Configurations;

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
				.HasConversion(x => x.Value, x => new FirstName(x))
            .HasColumnType("nvarchar(256)");

        builder.Property(u => u.Lastname)
				.HasConversion(x => x.Value, x => new LastName(x))
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
            .HasName("UsersIX_UserName")
            .IsUnique();

        builder.HasIndex(u => u.NormalizedUserName)
            .HasName("UsersIX_MormalizedUserName")
            .IsUnique();

        builder.HasIndex(u => u.Email)
            .HasName("UsersIX_Email");

        builder.HasIndex(u => u.NormalizedEmail)
            .HasName("UsersIX_NormalizedEmail");


        builder.ToTable("Users");
    }
}

﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Domain.Entities;

namespace SODP.DataAccess.Configurations
{
    public class TokenEntityConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.Ignore(p => p.Access);

            builder.Property(x => x.RefreshTokenKey)
                .HasColumnType("nvarchar(256)");

            builder.Property(x => x.Refresh)
                .HasColumnType("nvarchar(256)");

            builder.HasIndex(s => s.UserId)
                .HasName("TokensIX_User");

            builder.ToTable("Tokens");
        }
    }
}
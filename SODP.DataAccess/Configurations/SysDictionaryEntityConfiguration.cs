using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SODP.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.DataAccess.Configurations
{
    public class SysDictionaryEntityConfiguration : IEntityTypeConfiguration<SysDictionary>
    {
        public void Configure(EntityTypeBuilder<SysDictionary> builder)
        {
            builder.Property(x => x.Key)
                .HasColumnType("nvarchar(20)")
                .IsRequired();

            builder.Property(x => x.Value)
                .HasDefaultValue("")
                .HasColumnType("nvarchar(256)");

            builder.HasIndex(x => new { x.Key, x.Value })
                .HasName("IX_KeyValue");

            builder.ToTable("SysDictionary");
        }
    }
}

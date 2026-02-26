using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LibraryMinimalAPI.Persistence.Configurations
{
    public sealed class MemberTypeEntityConfiguration : IEntityTypeConfiguration<MemberType>
    {
        public void Configure(EntityTypeBuilder<MemberType> builder)
        {
            builder.ToTable("MemberType");
            builder.HasKey(x=> x.ID);
            builder.HasMany(m => m.Members)
                    .WithOne(m => m.MemberType)
                    .HasForeignKey(m => m.ID);
        }
    }
}

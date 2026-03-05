using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMinimalAPI.Persistence.Configurations
{
    public sealed class MemberEntityConfiguration : IEntityTypeConfiguration<Members>
    {
        public void Configure(EntityTypeBuilder<Members> builder)
        {
            builder.ToTable("Members");
            builder.HasKey(m=> m.ID);

            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            //builder.HasMany(m => m.BookIssueDetails)
            //    .WithOne(m => m.Members)
            //    .HasForeignKey(m => m.MemberID);


            //  Members BookIssueDetails relationship
            builder.HasMany(m => m.BookIssueDetails)
                   .WithOne(b => b.Members)
                   .HasForeignKey(b => b.MemberID);

            
            builder.HasOne(m => m.MemberType)
                   .WithMany(mt => mt.Members)
                   .HasForeignKey(m => m.MemberTypeID)
                   .HasPrincipalKey(mt => mt.ID);
        }
    }
}

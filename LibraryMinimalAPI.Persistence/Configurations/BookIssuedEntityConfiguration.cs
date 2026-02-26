using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMinimalAPI.Persistence.Configurations
{
    public sealed class BookIssuedEntityConfiguration : IEntityTypeConfiguration<BookIssueDetails>
    {
        public void Configure(EntityTypeBuilder<BookIssueDetails> builder)
        {
            builder.HasKey(b => b.ID);
            builder.Property(b => b.BookPrice)
                .HasColumnType("decimal(6,2)")
                .IsRequired();

            builder.Property(b => b.IssueDate)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(b => b.RenewDate)
                .IsRequired();

            builder.Property(b => b.Name);

            builder.Property(b => b.ReturnDate)
                .IsRequired();

            builder.HasOne(b => b.BookDetails)
                .WithMany(b => b.BookIssueDetails)
                .HasForeignKey(b => b.BookID)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(b => b.Members)
                .WithMany(m => m.BookIssueDetails)
                .HasForeignKey(b => b.MemberID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

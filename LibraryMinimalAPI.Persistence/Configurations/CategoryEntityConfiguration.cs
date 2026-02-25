using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMinimalAPI.Persistence.Configurations
{
    public sealed class CategoryEntityConfiguration: IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(c=> c.Id);
                builder
                    .Property(c => c.BookDetails)
                    .IsRequired()
                    .HasMaxLength(100);
            builder
                .HasMany(c => c.BookDetails)
                .WithOne(b => b.Categories)
                .HasForeignKey(c => c.Id);

        }
    }
}

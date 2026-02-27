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
            builder.ToTable("Categories");
            builder.HasKey(c=> c.ID);
                builder
                    .Property(c => c.BookCategory)
                    .IsRequired()
                    .HasMaxLength(100);
            builder
                .HasMany(c => c.BookDetails)
                .WithOne(b => b.Categories)
                .HasForeignKey(c => c.ID);

        }
    }
}

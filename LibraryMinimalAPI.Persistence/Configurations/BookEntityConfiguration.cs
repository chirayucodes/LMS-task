using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace LibraryMinimalAPI.Persistence.Configurations
{
    public sealed class BookEntityConfiguration : IEntityTypeConfiguration<BookDetails>
    {
        public void Configure(EntityTypeBuilder<BookDetails> builder)
        {
            builder.ToTable("BookDetails");

            builder.HasKey(b => b.Id);

            builder
                .Property(b => b.BookTitle)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(b => b.AuthorName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(b => b.PublisherName)
                .IsRequired()
                .HasMaxLength(100);
            builder
                .Property(b => b.BookPrice)
                .HasPrecision(6, 2)
                .IsRequired();


            //Relationship

            builder.HasOne(b => b.Categories)
                    .WithMany(c => c.BookDetails)
                    .IsRequired()
                    .HasForeignKey(b => b.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

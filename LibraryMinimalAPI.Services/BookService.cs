using LibraryMinimalAPI.Core.Dtos;
using LibraryMinimalAPI.Core.Requests;
using LibraryMinimalAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryMinimalAPI.Services
{
    public sealed class BookService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<BookService> _logger;

        public BookService(AppDbContext context, ILogger<BookService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<BookDTO> GetBooks(string? keyword = null)
        {
            var query = _context.BookDetails.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(b => b.BookTitle.Contains(keyword));
            }

            IList<BookDTO> books = query
                .Include(b => b.Categories)
                .Select(b => new BookDTO(
                    b.ID,
                    b.BookTitle,
                    b.AuthorName,
                    b.PublisherName,
                    b.BookPrice,
                    b.Categories.BookCategory
                )).ToArray();
            return new ReadOnlyCollection<BookDTO>(books);
        }

        public BookDTO? GetBookByID(int id)
        {
            var book = _context.BookDetails.FirstOrDefault(b => b.ID == id);
            if (book is null) return null;

            return new BookDTO(
                book.ID,
                book.BookTitle,
                book.AuthorName,
                book.PublisherName,
                book.BookPrice,
                 _context.Categories
                    .Where(c => c.ID == book.CategoryID)
                    .Select(c => c.BookCategory)
                    .FirstOrDefault() ?? string.Empty
                );
        }

        public BookDTO? PostBookRequest(PostBookRequest request)
        {
            try
            {
                var book = new BookDetails
                {
                    BookTitle = request.BookTitle,
                    AuthorName = request.AuthorName,
                    PublisherName = request.PublisherName,
                    BookPrice = request.BookPrice,
                    CategoryID = request.CategoryID
                };

                _context.BookDetails.Add(book);

                _context.SaveChanges();

                var bookDto = new BookDTO(
                    book.ID,
                    book.BookTitle,
                    book.AuthorName,
                    book.PublisherName,
                    book.BookPrice,
                     _context.Categories
                    .Where(c => c.ID == book.CategoryID)
                    .Select(c => c.BookCategory)
                    .FirstOrDefault() ?? string.Empty

                );
                return bookDto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex,
                    "Error while creating a Book.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while creating a Book with name {@BookTitle}.", request);
            }

            return null;

        }
    }
}

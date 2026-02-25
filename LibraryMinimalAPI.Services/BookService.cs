using LibraryMinimalAPI.Core.Dtos;
using LibraryMinimalAPI.Persistence;
using Microsoft.Extensions.Logging;

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

        public IEnumerable<BookDTO> GetBooks()
        {
            IReadOnlyList<BookDTO> BookDetails = _context.BookDetails
                .Select(b => new BookDTO(
                    b.Id,
                    b.BookTitle,
                    b.AuthorName,
                    b.PublisherName,
                    b.BookPrice,
                    b.CategoryId
                )).ToList();
            return BookDetails;
        }

        public BookDTO? GetBookByID(int id)
        {
            var book = _context.BookDetails.FirstOrDefault(b => b.Id == id);
            if (book is null) return null;

            return new BookDTO(
                book.Id,
                book.BookTitle,
                book.AuthorName,
                book.PublisherName,
                book.BookPrice,
                book.CategoryId
                );
        }


    }
}

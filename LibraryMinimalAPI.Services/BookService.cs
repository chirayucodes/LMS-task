using LibraryMinimalAPI.Core.Dtos;
using LibraryMinimalAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

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

        public IEnumerable<BookDTO> GetBooks(string? keyword= null)
        {
            var query = _context.BookDetails.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(b => b.BookTitle.Contains(keyword));
            }

            IList<BookDTO> books = query
                .Include(b => b.Categories)
                .Select(b => new BookDTO(
                    b.Id,
                    b.BookTitle,
                    b.AuthorName,
                    b.PublisherName, 
                    b.BookPrice,
                    b.CategoryId 
                )).ToArray();
            return new ReadOnlyCollection<BookDTO>(books);
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

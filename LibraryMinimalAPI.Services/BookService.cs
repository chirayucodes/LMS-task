using System.Collections.ObjectModel;
using LibraryMinimalAPI.Core.Dtos;
using LibraryMinimalAPI.Core.Requests;
using LibraryMinimalAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibraryMinimalAPI.Services;

public sealed class BookService
{
    private readonly AppDbContext _context;
    private readonly ILogger<BookService> _logger;

    public BookService(AppDbContext context, ILogger<BookService> logger)
    {
        _context = context;
        _logger = logger;
    }


    public IEnumerable<BookDTO> GetBooksList()
    {
        _logger.LogInformation("GetBooksList HIT");

        IReadOnlyList<BookDTO> books = _context.BookDetails
            //.Include(b => b.Categories)
            .Select(b => new BookDTO(
                b.ID,
                b.BookTitle,
                b.AuthorName,
                b.PublisherName,
                b.BookPrice,
                b.Categories.BookCategory
            ))
            .ToList();

        return books;
    }

    public IEnumerable<BookDTO> GetBookBySearch(string? keyword = null)
    {
        _logger.LogInformation("GetBookBySearch HIT");
        IQueryable<BookDetails> query = _context.BookDetails.AsQueryable();
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
        BookDetails? book = _context.BookDetails.FirstOrDefault(b => b.ID == id);
        if (book is null)
        {
            return null;
        }

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
            BookDetails book = new()
            {
                BookTitle = request.BookTitle,
                AuthorName = request.AuthorName,
                PublisherName = request.PublisherName,
                BookPrice = request.BookPrice,
                CategoryID = request.CategoryID
            };

            _context.BookDetails.Add(book);

            _context.SaveChanges();

            BookDTO bookDto = new(
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

    public BookDTO? DeleteBook(int ID)
    {
        try
        {
            BookDetails? book = _context.BookDetails.FirstOrDefault(b => b.ID == ID);

            if (book is null)
            {
                throw new ConflictException($"Cannot find this Id {ID}");
            }

            _context.BookDetails.Remove(book);

            _context.SaveChanges();

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
        catch (ConflictException ex)
        {
            _logger.LogError(ex, "Error while creating a state with BookId {Id}. Some conflicts occured.",
                ID);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex,
                "Error while Deleting a Book.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while Deleting a Book with name {@BookName}.", ID);
        }

        return null;
    }
}

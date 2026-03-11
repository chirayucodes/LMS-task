using System.Collections.ObjectModel;
using LibraryMinimalAPI.Core.Dtos;
using LibraryMinimalAPI.Core.Requests;
using LibraryMinimalAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibraryMinimalAPI.Services;

public sealed class BookIssuedService
{
    private readonly AppDbContext _DbContext;
    private readonly ILogger<BookIssuedService> _logger;

    public BookIssuedService(AppDbContext dbcontext, ILogger<BookIssuedService> logger)
    {
        _DbContext = dbcontext;
        _logger = logger;
    }

    public IEnumerable<BookIssuedDTO> GetBookIssued() 
    {
        IReadOnlyList<BookIssuedDTO> BooksIssued = _DbContext.BookIssueDetails
            .Include(b => b.BookDetails)
            .Include(b => b.Members)
            .Select(b => new BookIssuedDTO
            (
                b.ID,
                b.BookDetails.BookTitle,
                b.Members.Name,
                b.IssueDate,
                b.RenewDate,
                b.ReturnDate
            ))
            .ToList();

        return BooksIssued;
    }

    public IEnumerable<BookIssuedDTO> GetBookIssuedByMemberName(string? members)

    {
        IQueryable<BookIssueDetails> query = _DbContext.BookIssueDetails.AsQueryable();

        if (!string.IsNullOrWhiteSpace(members))
        {
            query = query.Where(b => b.Members.Name.Contains(members));
        }

        List<BookIssuedDTO> result = query
            .Include(b => b.Members)
            .Select(b => new BookIssuedDTO
            (
                b.ID,
                b.BookDetails.BookTitle,
                b.Members.Name,
                b.IssueDate,
                b.RenewDate,
                b.ReturnDate
            )).ToList();

        return new ReadOnlyCollection<BookIssuedDTO>(result);
    }

    public BookIssuedDTO? GetBookIssuedByMemberID(int MemberID)
    {
        BookIssuedDTO? issuedBook = _DbContext.BookIssueDetails
            .Where(b => b.MemberID == MemberID)
            .Select(b => new BookIssuedDTO(
                b.ID,
                b.BookDetails.BookTitle,
                b.Members.Name,
                b.IssueDate,
                b.RenewDate,
                b.ReturnDate
            ))
            .FirstOrDefault();

        return issuedBook;
    }

    public BookIssuedDTO? CreateBookIssueRequest(PostBookIssuedRequest request)
    {
        try
        {
            BookIssueDetails? bookIssueDetails = new()
            {
                BookID = request.BookID,
                MemberID = request.MemberID,
                IssueDate = request.IssueDate,
                RenewDate = request.RenewDate,
                ReturnDate = request.ReturnDate
            };

            if (bookIssueDetails == null)
            {
                throw new Exception("Failed to create a book from the provided request.");
            }

            if (!_DbContext.BookDetails.Any(b => b.ID == bookIssueDetails.BookID))
            {
                throw new ConflictException($"Book with ID {bookIssueDetails.BookID} does not exist.");
            }

            if (!_DbContext.Members.Any(m => m.ID == bookIssueDetails.MemberID))
            {
                throw new ConflictException($"Member with ID {bookIssueDetails.MemberID} does not exist.");
            }

            _DbContext.BookIssueDetails.Add(bookIssueDetails);
            _DbContext.SaveChanges();

            BookIssuedDTO CreatedIssuedBook = new(
                bookIssueDetails.ID,
                _DbContext.BookDetails
                    .Where(b => b.ID == bookIssueDetails.BookID)
                    .Select(b => b.BookTitle)
                    .FirstOrDefault() ?? string.Empty,
                _DbContext.Members
                    .Where(m => m.ID == bookIssueDetails.MemberID)
                    .Select(m => m.Name)
                    .FirstOrDefault() ?? string.Empty,
                bookIssueDetails.IssueDate,
                bookIssueDetails.RenewDate,
                bookIssueDetails.ReturnDate
            );
            return CreatedIssuedBook;
        }

        catch (DbUpdateException ex)
        {
            _logger.LogError(ex,
                "Error while creating a Book.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while creating a BookIssue.");
        }

        return null;
    }
}

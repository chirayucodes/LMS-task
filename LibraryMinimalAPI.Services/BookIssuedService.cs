using LibraryMinimalAPI.Core.Dtos;
using LibraryMinimalAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace LibraryMinimalAPI.Services
{
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
                 b.ReturnDate,
                 b.BookPrice
                ))
                .ToList();

            return BooksIssued;
        }

        public IEnumerable<BookIssuedDTO> GetBookIssuedByMemberName(string? members)

        {
            var query = _DbContext.BookIssueDetails.AsQueryable();
           
                if (!string.IsNullOrWhiteSpace(members))
                {
                    query = query.Where(b=>b.Members.Name.Contains(members));
                }

            var result = query
                .Include(b => b.Members)
                .Select(b => new BookIssuedDTO
                (
                        b.ID,
                        b.BookDetails.BookTitle,
                        b.Members.Name,
                        b.IssueDate,
                        b.RenewDate,
                        b.ReturnDate,
                        b.BookPrice

                )).ToList();
                
            return new ReadOnlyCollection<BookIssuedDTO>(result);

        }
        public BookIssuedDTO? GetBookIssuedByMemberID(int MemberID)
        {
            var issuedBook = _DbContext.BookIssueDetails
                .Where(b => b.MemberID == MemberID)
                .Select(b => new BookIssuedDTO(
                        b.ID,
                        b.BookDetails.BookTitle,
                        b.Members.Name,
                        b.IssueDate,
                        b.RenewDate,
                        b.ReturnDate,
                        b.BookPrice
                ))
                .FirstOrDefault();

            return issuedBook;

        }

    }             
}





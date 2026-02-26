using LibraryMinimalAPI.Core.Dtos;
using LibraryMinimalAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LibraryMinimalAPI.Web.Endpoints
{
    public static class BookEndpoint
    {
        public static IEndpointRouteBuilder MapBookEndpoints(this IEndpointRouteBuilder endpoints)
        {
            ArgumentNullException.ThrowIfNull(endpoints);
            IEndpointRouteBuilder bookGroup = endpoints.MapGroup("Books");
            endpoints.MapGet("books", GetBooks);
            endpoints.MapGet("books/{id:int}", GetBookByID);

            return endpoints;

        }
        private static Ok<IEnumerable<BookDTO>> GetBooks(BookService bookService, string? keyword)
        {
            IEnumerable<BookDTO> books = bookService.GetBooks(keyword);
            return TypedResults.Ok(books);
        }

        private static IResult GetBookByID(BookService bookService, int id)
        {
            BookDTO? book = bookService.GetBookByID(id);
            return book is null ? TypedResults.NotFound() : TypedResults.Ok(book);
        }
    }
}
using LibraryMinimalAPI.Core.Dtos;
using LibraryMinimalAPI.Core.Requests;
using LibraryMinimalAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LibraryMinimalAPI.Web.Endpoints
{
    public static class BookEndpoint
    {
        public static IEndpointRouteBuilder MapBookEndpoints(this IEndpointRouteBuilder endpoints)
        {
            ArgumentNullException.ThrowIfNull(endpoints);
            
            endpoints.MapGet("books", GetBooks);
            endpoints.MapGet("books/{id:int}", GetBookByID);
            endpoints.MapPost("books", PostBookRequest);

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

        private static IResult PostBookRequest(BookService bookServices, PostBookRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.BookTitle))
                return TypedResults.BadRequest("BookName is required.");

            var result = bookServices.PostBookRequest(request);
            return result is null
                ? TypedResults.Problem("There was some problem. See log for more details.")
                : TypedResults.Ok(result);
        }
    }
}
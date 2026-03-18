using LibraryMinimalAPI.Core.Dtos;
using LibraryMinimalAPI.Core.Requests;
using LibraryMinimalAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LibraryMinimalAPI.Web.Endpoints;

public static class BookEndpoint
{
    public static IEndpointRouteBuilder MapBookEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        endpoints.MapGet("/books", GetBooks); // GET all
        endpoints.MapGet("/books/search", GetBookBySearch); // search
        endpoints.MapGet("/books/{ID:int}", GetBookByID);
        endpoints.MapPost("/books", PostBookRequest);
        endpoints.MapDelete("{ID:int}", Delete);

        return endpoints;
    }

    private static Ok<IEnumerable<BookDTO>> GetBooks(BookService bookService)
    {
        IEnumerable<BookDTO> books = bookService.GetBooksList();

        return TypedResults.Ok(books);
    }

    private static Ok<IEnumerable<BookDTO>> GetBookBySearch(BookService bookService, string? keyword)
    {
        IEnumerable<BookDTO> books = bookService.GetBookBySearch(keyword);
        return TypedResults.Ok(books);
    }

    private static IResult GetBookByID(BookService bookService, int ID)
    {
        BookDTO? book = bookService.GetBookByID(ID);
        return book is null ? TypedResults.NotFound() : TypedResults.Ok(book);
    }

    private static IResult PostBookRequest(BookService bookServices, PostBookRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.BookTitle))
        {
            return TypedResults.BadRequest("BookName is required.");
        }

        BookDTO? result = bookServices.PostBookRequest(request);
        return result is null
            ? TypedResults.Problem("There was some problem. See log for more details.")
            : TypedResults.Ok(result);
    }
    private static IResult Delete(BookService bookServices, int ID)
    {
        BookDTO? result = bookServices.DeleteBook(ID);

        return result is null
            ? TypedResults.Problem("There was some problem. See log for more details.")
            : TypedResults.Ok(result);
    }
}

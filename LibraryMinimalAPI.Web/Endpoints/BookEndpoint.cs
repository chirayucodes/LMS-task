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


            endpoints.MapGet("books", GetBooks);
<<<<<<< HEAD
            endpoints.MapGet("books/{id:int}", GetBookByID);
=======
            endpoints.MapGet("books/{id}", GetBookByID);
>>>>>>> d7feafde1c75a260c1e71ee5f0d0cbd6338de486

            return endpoints;

        }
        private static Ok<IEnumerable<BookDTO>> GetBooks(BookService bookService)
        {
            IEnumerable<BookDTO> books = bookService.GetBooks();
            return TypedResults.Ok(books);
        }

        private static IResult GetBookByID(BookService bookService, int id)
        {
            BookDTO? book = bookService.GetBookByID(id);
            return book is null ? TypedResults.NotFound() : TypedResults.Ok(book);
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> d7feafde1c75a260c1e71ee5f0d0cbd6338de486

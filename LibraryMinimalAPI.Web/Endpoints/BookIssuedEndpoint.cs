using LibraryMinimalAPI.Core.Dtos;
using LibraryMinimalAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LibraryMinimalAPI.Web.Endpoints
{
    public static class BookIssuedEndpoint
    {
        public static IEndpointRouteBuilder MapBookIssuedGroup(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapGroup("issued");
        }

        public static IEndpointRouteBuilder MapBookIssuedEndpoints(this IEndpointRouteBuilder endpoints)
        {
            ArgumentNullException.ThrowIfNull(endpoints);

            IEndpointRouteBuilder BookIssuedGroup = endpoints.MapBookIssuedGroup();
           
            BookIssuedGroup.MapGet ("", GetBookIssued);
            BookIssuedGroup.MapGet("Search", GetBookIssuedByMemberName);
            //BookIssuedGroup.MapGet("member/{MemberID:int}/BookIssued", GetBookIssuedByMemberID)

            return endpoints;
        }

        private static Ok<IEnumerable<BookIssuedDTO>> GetBookIssued(BookIssuedService bookIssuedService)
        { 
            var BookIssued = bookIssuedService.GetBookIssued();
            return TypedResults.Ok(BookIssued);

        }
        private static IResult GetBookIssuedByMemberName(BookIssuedService bookIssuedServices, string members)
        {
            var BookIssued = bookIssuedServices.GetBookIssuedByMemberName(members);

            return BookIssued is null ? TypedResults.NotFound("MemberName Not Found") : TypedResults.Ok(BookIssued);
        }

    }

}

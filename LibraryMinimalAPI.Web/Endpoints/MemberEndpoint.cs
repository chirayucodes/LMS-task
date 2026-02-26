using LibraryMinimalAPI.Core.Dtos;
using LibraryMinimalAPI.Services;

namespace LibraryMinimalAPI.Web.Endpoints
{
    public static class MemberEndpoint
    {
        public static IEndpointRouteBuilder MapMemberGroup(this IEndpointRouteBuilder endpoints)
        {
            return endpoints
            .MapGroup("Members");
        }
        public static IEndpointRouteBuilder MapMemberEndpoints(this IEndpointRouteBuilder endpoints)
        {
            ArgumentNullException.ThrowIfNull(endpoints);
            IEndpointRouteBuilder MemberGroup = endpoints.MapMemberGroup();
            MemberGroup.MapGet("", GetMembers);
            MemberGroup.MapGet("{id:int}", GetMemberTypes);
            return MemberGroup;

        }
        private static IResult GetMembers(MemberService memberService)
        {
            ArgumentNullException.ThrowIfNull(memberService);
            IEnumerable<MembersDTO> members = memberService.GetMembers();
            return TypedResults.Ok(members);
        }

        private static IResult GetMemberTypes(MemberService memberService, int id) 
        {
            MemberTypeDTO? member = memberService.GetMemberType(id);
            return member is null? TypedResults.NotFound() : TypedResults.Ok(member);
        }
        
        
    }

}

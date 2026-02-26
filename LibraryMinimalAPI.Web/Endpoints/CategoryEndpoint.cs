using LibraryMinimalAPI.Core.Dtos;
using LibraryMinimalAPI.Services;

namespace LibraryMinimalAPI.Web.Endpoints
{
    public static class CategoryEndpoint
    {
        public static IEndpointRouteBuilder MapCategoryGroup(this IEndpointRouteBuilder endpoints)
        {
            return endpoints
                .MapGroup("Categories");

        }

        public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder endpoints)
        {
            ArgumentNullException.ThrowIfNull(endpoints);
            IEndpointRouteBuilder categoryGroup = endpoints.MapCategoryGroup();
            categoryGroup.MapGet("", GetCategories);
            categoryGroup.MapGet("{id}", GetCategoryBook);

            return endpoints;
        }

        private static IResult GetCategories(CategoryService categoryService)
        {
            IEnumerable<CategoryDTO> categories = categoryService.GetCategories();
            return TypedResults.Ok(categories);
        }

        private static IResult GetCategoryBook(CategoryService categoryService, int id)
        {
            CategoryDTO? category = categoryService.GetCategoryByID(id);
            return category is null ? TypedResults.NotFound() : TypedResults.Ok(category);
        }
    }
}

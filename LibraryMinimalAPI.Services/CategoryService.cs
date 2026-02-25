using LibraryMinimalAPI.Core.Dtos;
using LibraryMinimalAPI.Persistence;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace LibraryMinimalAPI.Services
{
    public sealed class CategoryService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(AppDbContext dbContext, ILogger<CategoryService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            IList<CategoryDTO> categories = _dbContext.Categories
                .Select(c => new CategoryDTO
                (
                 c.Id,
                 c.BookCategory))
                .ToArray();
            return new ReadOnlyCollection<CategoryDTO>(categories);
        }
    }
}

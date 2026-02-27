using LibraryMinimalAPI.Core.Dtos;
using LibraryMinimalAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;
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
                 c.ID,
                 c.BookCategory))
                .ToList();
            return new ReadOnlyCollection<CategoryDTO>(categories);
        }
        public CategoryDTO? GetCategoryByID(int id)
        {
            CategoryDTO? category = _dbContext.Categories
                .Where(c => c.ID == id)
                .Select(c => new CategoryDTO
                (
                 c.ID,
                 c.BookCategory))
                .FirstOrDefault();
            return category;
        }
    }   
}

namespace LibraryMinimalAPI.Core.Dtos
{
    public sealed class BookCategoryDTO
        (
            int ID,
            string BookCategory,
            IReadOnlyList<BookDTO> BookDetails
        )
    {
        public int ID { get; } = ID;
        public string BookCategory { get; } = BookCategory;

        public IReadOnlyList<BookDTO> BookDetails { get; } = BookDetails ?? throw new ArgumentException(nameof(BookDetails));
    }
}

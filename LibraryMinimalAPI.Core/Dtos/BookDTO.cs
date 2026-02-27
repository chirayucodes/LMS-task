namespace LibraryMinimalAPI.Core.Dtos
{
    public sealed class BookDTO
        (
        int id,
        string BookTitle,
        string AuthorName,
        string PublisherName,
        decimal BookPrice,
        string BookCategory
        )
    {
        public int Id { get; } = id;
        public string BookTitle { get; } = BookTitle;
        public string AuthorName { get; } = AuthorName;
        public string PublisherName { get; } = PublisherName;
        public decimal BookPrice { get; } = BookPrice;
        public string BookCategory { get; } = BookCategory;
    }
}

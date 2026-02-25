namespace LibraryMinimalAPI.Core.Dtos
{
    public sealed class BookDTO
        (
        int id,
        string BookTitle,
        string AuthorName,
        string PublisherName,
        decimal BookPrice,
        int CategoryId
        )
    {
        public int Id { get; } = id;
        public string BookTitle { get; } = BookTitle;
        public string AuthorName { get; } = AuthorName;
        public string PublisherName { get; } = PublisherName;
        public decimal BookPrice { get; } = BookPrice;
        public int CategoryId { get; } = CategoryId;
    }
}

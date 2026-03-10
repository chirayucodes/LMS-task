namespace LibraryMinimalAPI.Core.Dtos;

public sealed class BookDTO(
    int ID,
    string BookTitle,
    string AuthorName,
    string PublisherName,
    decimal BookPrice,
    string BookCategory
)
{
    public int ID { get; } = ID;
    public string BookTitle { get; } = BookTitle;
    public string AuthorName { get; } = AuthorName;
    public string PublisherName { get; } = PublisherName;
    public decimal BookPrice { get; } = BookPrice;
    public string BookCategory { get; } = BookCategory;
}

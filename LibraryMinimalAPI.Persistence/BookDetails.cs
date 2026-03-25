namespace LibraryMinimalAPI.Persistence;

public class BookDetails
{
    public int ID { get; set; }
    public required string BookTitle { get; set; }
    public required string AuthorName { get; set; }
    public required string PublisherName { get; set; }
    public required decimal BookPrice { get; set; }
    public int CategoryID { get; set; }
    public Categories? Categories { get; set; }
    public IList<BookIssueDetails> BookIssueDetails { get; set; } = [];
}

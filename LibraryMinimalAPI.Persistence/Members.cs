namespace LibraryMinimalAPI.Persistence;

public sealed class Members
{
    public int ID { get; set; }

    public required string Name { get; set; }
    public int MemberTypeID { get; set; }

    public MemberType? MemberType { get; set; }

    public IList<BookIssueDetails> BookIssueDetails { get; set; } = [];
}

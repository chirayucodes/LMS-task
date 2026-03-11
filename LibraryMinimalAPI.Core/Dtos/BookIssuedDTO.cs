namespace LibraryMinimalAPI.Core.Dtos;

public sealed class BookIssuedDTO(
    int ID,
    string BookTitle,
    string Name, // member name
    DateOnly IssueDate,
    DateOnly RenewDate,
    DateOnly? ReturnDate
)
{
    public int ID { get; } = ID;
    public string BookTitle { get; } = BookTitle;
    public string Name { get; } = Name;
    public DateOnly IssueDate { get; } = IssueDate;
    public DateOnly RenewDate { get; } = RenewDate;
    public DateOnly? ReturnDate { get; } = ReturnDate;

}

namespace LibraryMinimalAPI.Core.Dtos;

public sealed class MembersDTO(
    int ID,
    string Name,
    int MemberTypeID,
    string TypeName,
    int MaxBooks
)
{
    public int ID { get; } = ID;
    public string Name { get; } = Name;
    public int MemberTypeID { get; } = MemberTypeID;
    public string TypeName { get; } = TypeName;

    public int MaxBooks { get; } = MaxBooks;
}

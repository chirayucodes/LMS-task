namespace LibraryMinimalAPI.Core.Dtos
{
    public sealed class CategoryDTO
         (
             int ID,
             string BookCategory
         )
    {
        public int ID { get; } = ID;
        public string BookCategory { get; } = BookCategory;
    }
}

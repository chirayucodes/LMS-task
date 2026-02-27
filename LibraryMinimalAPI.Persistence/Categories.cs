namespace LibraryMinimalAPI.Persistence
{
    public sealed class Categories
    {
        public int ID { get; set; }
        public required string BookCategory { get; set; }

        public IList<BookDetails> BookDetails { get; set; } = [];
    }
}

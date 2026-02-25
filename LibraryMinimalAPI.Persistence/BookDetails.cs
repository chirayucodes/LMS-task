using System.ComponentModel.DataAnnotations;

namespace LibraryMinimalAPI.Persistence
{
    public class BookDetails
    {
        [Key] public int Id { get; set; }
        public required string BookTitle { get; set; }
        public required string AuthorName { get; set; }
        public required string PublisherName { get; set; }
        public required decimal BookPrice { get; set; }
        public required int CategoryId { get; set; }
        public required Categories Categories { get; set; }
    }

}

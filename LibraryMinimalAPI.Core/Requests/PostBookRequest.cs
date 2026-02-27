using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMinimalAPI.Core.Requests
{
    public sealed class PostBookRequest
    {
        public required string BookTitle { get; init; }
        public required string AuthorName { get; init; }
        public required string PublisherName { get; init; }
        public decimal BookPrice { get; init; }
        public int CategoryID { get; init; }// FK

    }
}

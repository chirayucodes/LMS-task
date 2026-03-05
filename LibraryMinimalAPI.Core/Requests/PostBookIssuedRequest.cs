using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMinimalAPI.Core.Requests
{
    public sealed class PostBookIssuedRequest
    {
        public int ID { get; init; }
        public required int BookID { get; init; }
        public required int MemberID { get; init; }
        public DateOnly IssueDate { get; init; }
        public DateOnly RenewDate { get; init; }

        public DateOnly? ReturnDate { get; init; }
        public required decimal BookPrice { get; init; }

    }
}

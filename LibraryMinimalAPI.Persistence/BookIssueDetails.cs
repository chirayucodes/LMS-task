using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMinimalAPI.Persistence
{
    public sealed class BookIssueDetails
    {
        public int ID { get; set; } 

        public required int BookID { get; set; }
        public required int MemberID { get; set; }
        public required DateOnly IssueDate { get; set; }
        public required DateOnly ReturnDate { get; set; }
        public DateOnly RenewDate { get; set; }
        public required decimal BookPrice { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Reflection;
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
        public required Members Members { get; set; }

       // public MemberTypes MemberTypes { get; set; }

        public required BookDetails BookDetails { get; set; }

    }
}

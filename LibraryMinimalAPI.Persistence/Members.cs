using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LibraryMinimalAPI.Persistence
{
    public sealed class Members
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public int MemberTypeID { get; set; }

        public required MemberTypes MemberTypes { get; set; }

        public IList<BookIssueDetails> BookIssueDetails { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMinimalAPI.Persistence
{
    public sealed class MemberType
    {
        public int ID { get; set; }
        public required string TypeName { get; set; }
        public required int MaxBooks { get; set; }
        public IList<Members> Members { get; set; } = [];
    }
}

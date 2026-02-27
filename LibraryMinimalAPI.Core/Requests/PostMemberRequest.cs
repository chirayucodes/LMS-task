using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMinimalAPI.Core.Requests
{
    public sealed class PostMemberRequest
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public int MemberTypeID { get; init; } //FK

    }
}

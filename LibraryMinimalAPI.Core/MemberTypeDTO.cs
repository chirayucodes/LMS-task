using LibraryMinimalAPI.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LibraryMinimalAPI.Core
{
    public sealed class MemberTypeDTO
        (
            int ID,
            string TypeName,
            int MaxBooks,
            IReadOnlyList<MembersDTO> Members
        )
    {
        public int ID { get; } = ID;
        public string TypeName { get; } = TypeName;
        public int MaxBooks { get; } = MaxBooks;

        public IReadOnlyList<MembersDTO> Members { get; } = Members;
    }
}

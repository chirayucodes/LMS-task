using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMinimalAPI.Core.Dtos
{
    public sealed class MembersDTO
        (
            int ID, 
            string Name,
            int MemberTypeID
        )
    {
        public int ID { get; } = ID;
        public string Name { get; }= Name;
        public int MemberTypeID { get; }= MemberTypeID;
    }
}

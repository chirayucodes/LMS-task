using LibraryMinimalAPI.Core.Dtos;
using LibraryMinimalAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Text;

namespace LibraryMinimalAPI.Services
{
    public sealed class MemberService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<MemberService> _logger;

        public MemberService (AppDbContext dbContext, ILogger<MemberService> logger) 
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IEnumerable<MembersDTO> GetMembers() 
        {
            IList<MembersDTO> members = _dbContext.Members
                .Select(m=> new MembersDTO
                (   
                    m.ID,
                    m.Name,
                    m.MemberTypeID)
                
                ).ToArray();
            
            return new ReadOnlyCollection<MembersDTO>(members);
        }
        
        public MemberTypeDTO? GetMemberType(int id)
        {
            MemberType? member = _dbContext.MemberType
                .Include(m => m.Members)
                .FirstOrDefault(m => m.ID == id);
           
            if(member is null) 
            {
                return null;
            }

            IImmutableList<MembersDTO> members = member.Members
                .Select(m => new MembersDTO
                (
                    m.ID,
                    m.Name,
                    m.MemberTypeID
                )).ToImmutableList();

            return new MemberTypeDTO(member.ID, member.TypeName, member.MaxBooks, members);
        }
    }
}

using LibraryMinimalAPI.Core.Dtos;
using LibraryMinimalAPI.Core.Requests;
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
                .Include(u=>u.MemberType)
                .Select(m=> new MembersDTO
                (   
                    m.ID,
                    m.Name,
                    m.MemberTypeID,
                    m.MemberType.TypeName
                )).ToArray();
            
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
                    m.MemberTypeID,
                    _dbContext.MemberType
                        .Where(u => u.ID == member.ID)
                        .Select(u => u.TypeName)
                        .FirstOrDefault() ?? string.Empty
                )).ToImmutableList();

            return new MemberTypeDTO(member.ID, member.TypeName, member.MaxBooks, members);
        }

        public MembersDTO? CreateMember(PostMemberRequest request)
        {
            try
            {
                var member = new Members
                {
                    Name = request.Name,
                    MemberTypeID = request.MemberTypeID
                };

                _dbContext.Members.Add(member);
                _dbContext.SaveChanges();

                var result = new MembersDTO(
                    member.ID,
                    member.Name,
                    member.MemberTypeID,
                    _dbContext.MemberType
                        .Where(u => u.ID == member.MemberTypeID)
                        .Select(u => u.TypeName)
                        .FirstOrDefault() ?? string.Empty
                );

                return result;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex,
                    "Error while creating a User.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while creating a User with name {@Name}.", request);
            }

            return null;
        }
    }
}

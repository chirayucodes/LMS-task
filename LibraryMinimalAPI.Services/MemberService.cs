using LibraryMinimalAPI.Core.Dtos;
using LibraryMinimalAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
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
                .Select(m=> new MembersDTO)
        }
    }
}

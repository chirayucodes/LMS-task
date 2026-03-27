using System.Collections.Immutable;
using System.Collections.ObjectModel;
using LibraryMinimalAPI.Core.Dtos;
using LibraryMinimalAPI.Core.Requests;
using LibraryMinimalAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibraryMinimalAPI.Services;

public sealed class MemberService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<MemberService> _logger;

    public MemberService(AppDbContext dbContext, ILogger<MemberService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public IEnumerable<MembersDTO> GetMembers()
    {
        IList<MembersDTO> members = _dbContext.Members
            .Include(u => u.MemberType)
            .Select(m => new MembersDTO
            (
                m.ID,
                m.Name,
                m.MemberTypeID,
                m.MemberType.TypeName,
                m.MemberType.MaxBooks
            )).ToArray();

        return new ReadOnlyCollection<MembersDTO>(members);
    }

    public MemberTypeDTO? GetMemberType(int id)
    {
        MemberType? member = _dbContext.MemberType
            .Include(m => m.Members)
            .FirstOrDefault(m => m.ID == id);

        if (member is null)
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
                    .FirstOrDefault() ?? string.Empty,
                _dbContext.MemberType
                    .Where(u => u.ID == member.ID)
                    .Select(u => u.MaxBooks)
                    .FirstOrDefault()
            )).ToImmutableList();

        return new MemberTypeDTO(member.ID, member.TypeName, member.MaxBooks, members);
    }

    public MembersDTO? CreateMember(PostMemberRequest request)
    {
        try
        {
            Members member = new() { Name = request.Name, MemberTypeID = request.MemberTypeID };

            _dbContext.Members.Add(member);
            _dbContext.SaveChanges();

            MembersDTO result = new(
                member.ID,
                member.Name,
                member.MemberTypeID,
                _dbContext.MemberType
                    .Where(u => u.ID == member.MemberTypeID)
                    .Select(u => u.TypeName)
                    .FirstOrDefault() ?? string.Empty,
                _dbContext.MemberType
                    .Where(u => u.ID == member.MemberTypeID)
                    .Select(u => u.MaxBooks)
                    .FirstOrDefault()
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

    public MembersDTO? UpdateMember(int Id, PostMemberRequest request)
    {
        try
        {
            //Members? member = _dbContext.Members.Include(m => m.MemberType)
            //    .FirstOrDefault(m => m.ID == Id); 
            Members? member = _dbContext.Members.Find(Id);
            if (member == null)
            {
                _logger.LogWarning("member with ID {Id} not found for update.", Id);
                return null;
            }

            member.Name = request.Name;
            member.MemberTypeID = request.MemberTypeID;

            _dbContext.SaveChanges();

            MembersDTO result = new(
                member.ID,
                member.Name,
                  member.MemberTypeID,
                  _dbContext.MemberType
                    .Where(u => u.ID == member.MemberTypeID)
                    .Select(u => u.TypeName)
                    .FirstOrDefault() ?? string.Empty,
                _dbContext.MemberType
                    .Where(u => u.ID == member.MemberTypeID)
                    .Select(u => u.MaxBooks)
                    .FirstOrDefault()
                );

            return result;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex,
                "Error while updating a member with ID {Id}.", Id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while updating a member with ID {Id} and name {@Name}.", Id, request);
        }

        return null;
    }

    public MembersDTO? GetMemberById(int id)
    {
        var member = _dbContext.Members
            .Where(m => m.ID == id)
            .Select(m => new MembersDTO(
                m.ID,
                m.Name,
                m.MemberTypeID,
                _dbContext.MemberType
                    .Where(mt => mt.ID == m.MemberTypeID)
                    .Select(mt => mt.TypeName)
                    .FirstOrDefault() ?? string.Empty,
                _dbContext.MemberType
                    .Where(mt => mt.ID == m.MemberTypeID)
                    .Select(mt => mt.MaxBooks)
                    .FirstOrDefault()
            ))
            .FirstOrDefault();

        return member;
    }

    public MembersDTO? DeleteMember(int ID)
    {
        try
        {
            Members? member = _dbContext.Members.Find(ID);
            if (member == null)
            {
                _logger.LogWarning("Member with ID {ID} not found for deletion.", ID);
                return null;
            }

            _dbContext.Members.Remove(member);
            _dbContext.SaveChanges();
            MembersDTO result = new(
                member.ID,
                member.Name,
                member.MemberTypeID,
                _dbContext.MemberType
                    .Where(u => u.ID == member.MemberTypeID)
                    .Select(u => u.TypeName)
                    .FirstOrDefault() ?? string.Empty,
                _dbContext.MemberType
                    .Where(u => u.ID == member.MemberTypeID)
                    .Select(u => u.MaxBooks)
                    .FirstOrDefault()
                );
            return result;

        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex,
                "Error while deleting a member with ID {ID}.", ID);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting a member with ID {ID}.", ID);

        }

        return null;
    }
}

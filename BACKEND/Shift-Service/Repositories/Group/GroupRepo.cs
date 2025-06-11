using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shift_Service.Data;

namespace Shift_Service.Repositories.Group
{
    public class GroupRepo : IGroupRepo
    {
        private readonly ShiftServiceContext _context;
        private readonly ILogger<GroupRepo> _logger;
        public GroupRepo(ShiftServiceContext context, ILogger<GroupRepo> logger)
        {
            this._context = context;
            _logger = logger;
        }

        public async Task<GroupResposne> AddGroup(GroupRequest group)
        {
            var shift = _context.Shifts.Find(group.ShiftId);

            if (shift.Role.ToString().Equals(group.Role))
            {
                var groupModel = group.Adapt<Models.Group>();
                var x = await _context.Groups.AddAsync(groupModel);
                await _context.SaveChangesAsync();
                return groupModel.Adapt<GroupResposne>();
            }
            throw new Exception("shift and group have different roles");

        }

        public IQueryable<Models.Group> GetAllGroups()
        {
            var groups = _context.Groups.Where(r => !r.IsDeleted).Include(r => r.Shift);

            return groups;
        }

        public async Task<Models.Group> GetGroupById(int id)
        {
            var group = await _context.Groups.Include(r => r.Shift).Where(r => !r.IsDeleted).FirstOrDefaultAsync(r => r.Id == id) ?? throw new Exception("shift with id " + id + "not found");
            return group;
        }

        public async Task DeleteGroupe(int id)
        {
            var group = await GetGroupById(id);
            group.IsDeleted = true;
            _context.Groups.Update(group);
            await _context.SaveChangesAsync();
        }

        public async Task<GroupResposne> UpdateGroup(int id, GroupRequest groupReq)
        {
            var shift = await GetGroupById(id);
            groupReq.Adapt(shift);
            await _context.SaveChangesAsync();
            return shift.Adapt<GroupResposne>();
        }
    }
}

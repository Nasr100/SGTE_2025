using Mapster;
using Shared.Dtos;
using Shift_Service.Data;

namespace Shift_Service.Repositories.Group
{
    public class GroupRepo
    {
        private readonly ShiftServiceContext _context;
        public GroupRepo(ShiftServiceContext context)
        {
            this._context = context;
        }

        public async Task<GroupResposne> AddShift(GroupRequest group)
        {
            var groupModel = group.Adapt<Models.Group>();
            var x = await _context.group.AddAsync(groupModel);
            await _context.SaveChangesAsync();
            return groupModel.Adapt<GroupResposne>();
        }
    }
}

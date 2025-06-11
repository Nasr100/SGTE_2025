using Gridify;
using Mapster;
using Shared.Dtos;
using Shift_Service.Repositories.Group;
using Shift_Service.Enums;
using Microsoft.EntityFrameworkCore;

namespace Shift_Service.Services.Group
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepo _GroupRepo;
        private readonly ILogger<GroupService> _logger;
        public GroupService(IGroupRepo GroupRepo, ILogger<GroupService> logger)
        {
            this._GroupRepo = GroupRepo;
            this._logger = logger;


        }

        public async Task<GroupResposne> AddGroup(GroupRequest group)
        {
            
            var groupResponse = await this._GroupRepo.AddGroup(group);
            return groupResponse;
        }

        public Paging<GroupResposne> GetAllGroups(GridifyQuery grifigy)
        {
            var groups = _GroupRepo.GetAllGroups().Gridify(grifigy);

            return groups.Adapt<Paging<GroupResposne>>();
        }

        public async Task<GroupResposne> GetGroupById(int id)
        {
            var group = await _GroupRepo.GetGroupById(id);
            _logger.LogError("GROUPE : "+group.Id);
            _logger.LogError("GROUPE : " + group.Shift.shift);
            return group.Adapt<GroupResposne>();
        }

        public async Task DeleteGroup(int id)
        {
            await _GroupRepo.DeleteGroupe(id);
        }

        public async Task<GroupResposne> UpdateGroup(int id, GroupRequest groupRequest)
        {
            var group = await _GroupRepo.UpdateGroup(id, groupRequest);
            return group;
        }

        public async Task<GroupResposne> GetGroupsByRole(string Role)
        {
            var groups = await _GroupRepo.GetAllGroups().Where(g => g.Role.ToString() == Role).ToListAsync();
            return groups.Adapt<GroupResposne>();
        }

        public async Task<GroupResposne> GetDriverGroupByshift(string shift)
        {
            var DriverGroups =  _GroupRepo.GetAllGroups().Where(g => g.Role.ToString() == "driver");
           var group =  await DriverGroups.FirstOrDefaultAsync(dg => dg.Shift.shift.ToString() == shift);

            //var group = await _GroupRepo.GetAllGroups().Where(g => g.Shift.shift.ToString().Equals(shift)).ToListAsync();
            //_logger.LogError("GROUP"+group.)
            return group.Adapt<GroupResposne>();
        }




    }
}

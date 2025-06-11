using Gridify;
using Shared.Dtos;

namespace Shift_Service.Services.Group
{
    public interface IGroupService
    {
        public Task<GroupResposne> AddGroup(GroupRequest group);
        public Paging<GroupResposne> GetAllGroups(GridifyQuery grifigy);
        public  Task<GroupResposne> GetGroupById(int id);
        public  Task DeleteGroup(int id);
        public Task<GroupResposne> UpdateGroup(int id, GroupRequest groupRequest);
        public Task<GroupResposne> GetDriverGroupByshift(string shift);

        public Task<GroupResposne> GetGroupsByRole(string Role);



    }
}


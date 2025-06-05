using Shared.Dtos;

namespace Shift_Service.Repositories.Group
{
    public interface IGroupRepo
    {
        public Task<GroupResposne> AddGroup(GroupRequest group);
        public IQueryable<Models.Group> GetAllGroups();
        public Task<Models.Group> GetGroupById(int id);
        public  Task DeleteGroupe(int id);
        public Task<GroupResposne> UpdateGroup(int id, GroupRequest groupReq);


    }
}

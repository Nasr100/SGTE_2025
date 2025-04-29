using Gridify;
using Shared.Dtos;

namespace User_Service.Services.Administration
{
    public interface IAdministrationService
    {
        public Task<AdministrationResponse> Add(AdministrationRequest administration);
        public Paging<AdministrationResponse> GetAll(GridifyQuery gridifyQuery);
        public Task<AdministrationResponse> GetById(int id);
        public Task Delete(int id);
        public Task<AdministrationResponse> Update(int id, AdministrationRequest administration);
        public  Task<AdministrationResponse> PartialUpdate(int id, Dictionary<string, object> updates);
        //public  Task<AdministrationResponse> GetByEmail(string email);





    }
}

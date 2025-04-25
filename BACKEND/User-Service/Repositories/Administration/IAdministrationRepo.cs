using Shared.Dtos;

namespace User_Service.Repositories.Administration
{
    public interface IAdministrationRepo
    {
        public  Task<AdministrationResponse> AddAdministration(AdministrationRequest administration);
        public Task<Models.Administration> GetAdministrationById(int id);
        public Task<List<Models.Administration>> GetAll();
        public  Task Delete(int id);
        public Task<AdministrationResponse> UpdateAdministration(int id, AdministrationRequest administration);
        public Task<AdministrationResponse> PartialUpdateAsync(int id, Dictionary<string, object> updates);
        //public  Task<AdministrationResponse> GetByEmail(string email);



    }
}

using Gridify;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using User_Service.Models;
using User_Service.Repositories.Administration;

namespace User_Service.Services.Administration
{
    public class AdministrationService : IAdministrationService
    {
        private readonly IAdministrationRepo _administrationService;
        public AdministrationService(IAdministrationRepo administrationRepo)
        {
            _administrationService = administrationRepo;
        }


        public async Task<AdministrationResponse> Add(AdministrationRequest administration)
        {
           var responseModel = await _administrationService.AddAdministration(administration);

            return responseModel;
        }
        public  Paging<AdministrationResponse> GetAll(GridifyQuery gridifyQuery)
        {
           
                var response = _administrationService.GetAll().Gridify(gridifyQuery);
                return response.Adapt<Paging<AdministrationResponse>>();
            
        
        }

        public async Task<AdministrationResponse> GetById(int id)
        {
            var response = await _administrationService.GetAdministrationById(id);
            return response.Adapt<AdministrationResponse>();

        }

        public async Task<AdministrationResponse> Update(int id, AdministrationRequest administration)
        {
            var response = await _administrationService.UpdateAdministration(id, administration);
            return response;
        }
        public async Task Delete(int id)
        {
             await _administrationService.Delete(id);
        }

        public async Task<AdministrationResponse> PartialUpdate(int id, Dictionary<string, object> updates)
        {
            var response = await _administrationService.PartialUpdateAsync(id, updates);
            return response;

        }
        //public async Task<AdministrationResponse> GetByEmail(string email)
        //{
        //    var response = await _administrationService.GetByEmail(email);
        //    return response;

        //}

    }
}

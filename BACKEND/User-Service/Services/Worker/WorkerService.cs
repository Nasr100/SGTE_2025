using Gridify;
using Mapster;
using Shared.Dtos;
using User_Service.Repositories.Driver;

namespace User_Service.Services.Worker
{
    public class WorkerService
    {
        private readonly IDriverRepo _adriverRepo;
        public WorkerService(IDriverRepo adriverRepo)
        {
            _adriverRepo = adriverRepo;
        }

        public async Task<DriverResponse> Add(DriverRequest driverRequest)
        {
            var responseModel = await _adriverRepo.AddDriver(driverRequest);

            return responseModel;
        }

        public Paging<DriverResponse> GetAll(GridifyQuery gridifyQuery)
        {
            var response = _adriverRepo.GetAll().Gridify(gridifyQuery);
            return response.Adapt<Paging<DriverResponse>>();
        }

        public async Task<DriverResponse> GetById(int id)
        {
            var response = await _adriverRepo.GetDriverById(id);
            return response.Adapt<DriverResponse>();
        }

        public async Task<DriverResponse> Update(int id, DriverRequest driverRequest)
        {
            var response = await _adriverRepo.UpdateDriver(id, driverRequest);
            return response;
        }
        public async Task Delete(int id)
        {
            await _adriverRepo.Delete(id);
        }

        public async Task<DriverResponse> PartialUpdate(int id, Dictionary<string, object> updates)
        {
            var response = await _adriverRepo.PartialUpdateAsync(id, updates);
            return response;
        }
    }
}

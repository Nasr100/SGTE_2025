using Gridify;
using Mapster;
using Shared.Dtos;
using User_Service.Repositories.Driver;

namespace User_Service.Services.Driver
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepo _driverRepo;
        public DriverService(IDriverRepo driverRepo)
        {
            _driverRepo = driverRepo;
        }

        public async Task<DriverResponse> Add(DriverRequest driverRequest)
        {
            var responseModel = await _driverRepo.AddDriver(driverRequest);

            return responseModel;
        }

        public Paging<DriverResponse> GetAll(GridifyQuery gridifyQuery)
        {
            var response = _driverRepo.GetAll().Gridify(gridifyQuery);
            return response.Adapt<Paging<DriverResponse>>();
        }

        public async Task<DriverResponse> GetById(int id)
        {
            var response = await _driverRepo.GetDriverById(id);
            return response.Adapt<DriverResponse>();

        }

        public async Task<DriverResponse> Update(int id, DriverRequest driverRequest)
        {
            var response = await _driverRepo.UpdateDriver(id, driverRequest);
            return response;
        }

        public async Task Delete(int id)
        {
            await _driverRepo.Delete(id);
        }

        public async Task<DriverResponse> PartialUpdate(int id, Dictionary<string, object> updates)
        {
            var response = await _driverRepo.PartialUpdateAsync(id, updates);
            return response;

        }
    }
}

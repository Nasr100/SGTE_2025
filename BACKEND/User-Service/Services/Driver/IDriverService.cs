using Gridify;
using Shared.Dtos;

namespace User_Service.Services.Driver
{
    public interface IDriverService
    {
        public Task<DriverResponse> Add(DriverRequest driverRequest);
        public Paging<DriverResponse> GetAll(GridifyQuery gridifyQuery);
        public Task<DriverResponse> GetById(int id);
        public Task<DriverResponse> Update(int id, DriverRequest driverRequest);
        public  Task Delete(int id);
        public Task<DriverResponse> PartialUpdate(int id, Dictionary<string, object> updates);

    }
}

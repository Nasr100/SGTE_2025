using Shared.Dtos;

namespace User_Service.Repositories.Driver
{
    public interface IDriverRepo
    {
        public Task<DriverResponse> AddDriver(DriverRequest driver);
        public Task<Models.Driver> GetDriverById(int id);
        public IQueryable<Models.Driver> GetAll();
        public  Task Delete(int id);
        public Task<DriverResponse> UpdateDriver(int id, DriverRequest driverRequest);
        public Task<DriverResponse> PartialUpdateAsync(int id, Dictionary<string, object> updates);




    }
}

using System.Reflection;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared.Dtos;
using User_Service.Data;

namespace User_Service.Repositories.Driver
{
    public class DriverRepo:IDriverRepo
    {
        private readonly UserServiceContext _context;

        public DriverRepo(UserServiceContext context)
        {
            _context = context;
        }

        public async Task<DriverResponse> AddDriver(DriverRequest driver)
        {
            var driverModel = driver.Adapt<Models.Driver>();
            await _context.Drivers.AddAsync(driverModel);
            await _context.SaveChangesAsync();
            return driverModel.Adapt<DriverResponse>();
        }

        public async Task<Models.Driver> GetDriverById(int id)
        {
            var driver = await _context.Drivers.FirstOrDefaultAsync(e => e.Id == id);
            if (driver == null)
            {
                throw new InvalidOperationException("administration member with the id " + id + "not found");
            }
            return driver;
        }

        public IQueryable<Models.Driver> GetAll()
        {
            var driver = _context.Drivers.Where(e => !e.Employee.IsDeleted).AsQueryable();
            return driver;
        }

        public async Task Delete(int id)
        {
            var driver = await GetDriverById(id);
            driver.Employee.IsDeleted = true;
            _context.Drivers.Update(driver);
            await _context.SaveChangesAsync();
        }

        public async Task<DriverResponse> UpdateDriver(int id, DriverRequest driverRequest)
        {
            var driver = await GetDriverById(id);
            driver = driverRequest.Adapt<Models.Driver>();
            _context.Drivers.Update(driver);
            await _context.SaveChangesAsync();
            return driver.Adapt<DriverResponse>();

        }

        public async Task<DriverResponse> PartialUpdateAsync(int id, Dictionary<string, object> updates)
        {
            var entity = await GetDriverById(id);
            foreach (var update in updates)
            {
                var property = entity.GetType().GetProperty(update.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property != null && property.CanWrite)
                {

                    try
                    {
                        var jsonValue = JsonConvert.SerializeObject(update.Value);
                        var convertedValue = JsonConvert.DeserializeObject(jsonValue, property.PropertyType);
                        property.SetValue(entity, convertedValue);

                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException(
                            $"Failed to convert value for property '{update.Key}'. Expected type: {property.PropertyType}. Error: {ex.Message}"
                        );
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Property '{update.Key}' not found or cannot be updated.");
                }
            }
            _context.Drivers.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Adapt<DriverResponse>();
        }
    }
}

using System.Reflection;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Dtos;
using User_Service.Data;
using User_Service.Models;
namespace User_Service.Repositories.Administration
{
    public class AdministrationRepo : IAdministrationRepo
    {
        private readonly UserServiceContext _context;
        private readonly ILogger<AdministrationRepo> _logger;

        public AdministrationRepo(UserServiceContext context, ILogger<AdministrationRepo> logger)
        {
            _context = context;
            _logger = logger;
        }
      public async Task<AdministrationResponse> AddAdministration(AdministrationRequest administration)
        {
            var administrationModel = administration.Adapt<Models.Administration>();
            await _context.Administrations.AddAsync(administrationModel);
           await _context.SaveChangesAsync();
            return administrationModel.Adapt<AdministrationResponse>();
        }

        public async Task<Models.Administration> GetAdministrationById(int id)
        {
           var administration =  await _context.Administrations.Include(e=>e.Employee.Roles).FirstOrDefaultAsync(e=>e.Id == id);
            if (administration == null)
            {
                throw new InvalidOperationException("administration member with the id "+id+"not found");
            }
            return administration;
        }

        public  IQueryable<Models.Administration> GetAll()
        {
            var administrations = _context.Administrations.Where(e=>!e.Employee.IsDeleted).Include(e=>e.Employee.Roles).AsQueryable();
            return administrations;
        }

        public async Task Delete(int id)
        {
            var member = await GetAdministrationById(id);
            _logger.LogInformation("member = "+member.EmployeeId);
            member.Employee.IsDeleted = true;
             _context.Administrations.Update(member);
            await _context.SaveChangesAsync();
        }

        public async Task<AdministrationResponse> UpdateAdministration(int id, AdministrationRequest administration)
        {
            var member = await GetAdministrationById(id);
            member = administration.Adapt<Models.Administration>();
            _context.Administrations.Update(member);
            await _context.SaveChangesAsync();
            return member.Adapt<AdministrationResponse>();

        }

        public async Task<AdministrationResponse> PartialUpdateAsync(int id, Dictionary<string, object> updates)
        {
            var entity = await GetAdministrationById(id);
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
            _context.Administrations.Update(entity);
            await _context.SaveChangesAsync(); 
            return entity.Adapt<AdministrationResponse>();
        }

        


    }
}

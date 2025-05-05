using System.Reflection;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared.Dtos;
using User_Service.Data;

namespace User_Service.Repositories.Worker
{
    public class WorkerRepo:IWorkerRepo
    {
        private readonly UserServiceContext _context;
        public WorkerRepo(UserServiceContext context)
        {
            _context = context;
        }

        public async Task<WorkerResponse> AddWorker(WorkerRequest workerRequest)
        {
            var workerModel = workerRequest.Adapt<Models.Worker>();
            await _context.Workers.AddAsync(workerModel);
            await _context.SaveChangesAsync();
            return workerModel.Adapt<WorkerResponse>();
        }

        public async Task<Models.Worker> GetWorkerById(int id)
        {
            var worker = await _context.Workers.FirstOrDefaultAsync(e => e.Id == id);
            if (worker == null)
            {
                throw new InvalidOperationException("worker  with the id " + id + "not found");
            }
            return worker;
        }

        public IQueryable<Models.Worker> GetAll()
        {
            var worker = _context.Workers.Where(e => !e.Employee.IsDeleted).AsQueryable();
            return worker;
        }

        public async Task Delete(int id)
        {
            var member = await GetWorkerById(id);
            member.Employee.IsDeleted = true;
            _context.Workers.Update(member);
            await _context.SaveChangesAsync();
        }

        public async Task<WorkerResponse> UpdateAdministration(int id, WorkerRequest workerRequest)
        {
            var member = await GetWorkerById(id);
            member = workerRequest.Adapt<Models.Worker>();
            _context.Workers.Update(member);
            await _context.SaveChangesAsync();
            return member.Adapt<WorkerResponse>();

        }

        public async Task<WorkerResponse> PartialUpdateAsync(int id, Dictionary<string, object> updates)
        {
            var entity = await GetWorkerById(id);
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
            _context.Workers.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Adapt<WorkerResponse>();
        }
    }
}

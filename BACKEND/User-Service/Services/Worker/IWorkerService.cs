using Gridify;
using Shared.Dtos;

namespace User_Service.Services.Worker
{
    public interface IWorkerService
    {
        public Task<WorkerResponse> Add(WorkerRequest driverRequest);

        public Paging<WorkerResponse> GetAll(GridifyQuery gridifyQuery);
        public Task<WorkerResponse> GetById(int id);
        public Task<WorkerResponse> Update(int id, WorkerRequest driverRequest);
        public  Task Delete(int id);
        public Task<WorkerResponse> PartialUpdate(int id, Dictionary<string, object> updates);

    }
}

using Shared.Dtos;

namespace User_Service.Repositories.Worker
{
    public interface IWorkerRepo
    {
        public Task<WorkerResponse> AddWorker(WorkerRequest workerRequest);
        public  Task<Models.Worker> GetWorkerById(int id);
        public IQueryable<Models.Worker> GetAll();
        public Task Delete(int id);
        public Task<WorkerResponse> UpdateWorker(int id, WorkerRequest workerRequest);
        public Task<WorkerResponse> PartialUpdateAsync(int id, Dictionary<string, object> updates);

    }
}

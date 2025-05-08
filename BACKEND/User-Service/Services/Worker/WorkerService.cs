using Gridify;
using Mapster;
using Shared.Dtos;
using User_Service.Repositories.Driver;
using User_Service.Repositories.Worker;

namespace User_Service.Services.Worker
{
    public class WorkerService: IWorkerService
    {
        private readonly IWorkerRepo _workerRepo;
        public WorkerService(IWorkerRepo adriverRepo)
        {
            _workerRepo = adriverRepo;
        }

        public async Task<WorkerResponse> Add(WorkerRequest workerRequest)
        {
            var responseModel = await _workerRepo.AddWorker(workerRequest);

            return responseModel;
        }

        public Paging<WorkerResponse> GetAll(GridifyQuery gridifyQuery)
        {
            var response = _workerRepo.GetAll().Gridify(gridifyQuery);
            return response.Adapt<Paging<WorkerResponse>>();
        }

        public async Task<WorkerResponse> GetById(int id)
        {
            var response = await _workerRepo.GetWorkerById(id);
            return response.Adapt<WorkerResponse>();
        }

        public async Task<WorkerResponse> Update(int id, WorkerRequest workerRequest)
        {
            var response = await _workerRepo.UpdateWorker(id, workerRequest);
            return response;
        }
        public async Task Delete(int id)
        {
            await _workerRepo.Delete(id);
        }

        public async Task<WorkerResponse> PartialUpdate(int id, Dictionary<string, object> updates)
        {
            var response = await _workerRepo.PartialUpdateAsync(id, updates);
            return response;
        }
    }
}

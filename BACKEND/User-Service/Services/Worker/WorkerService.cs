using System;
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
        private readonly ILogger<WorkerService> _logger;
        private string RouteServiceBaseUrl = "https://localhost:7025/";
        private HttpClient client;
        public WorkerService(IWorkerRepo adriverRepo, ILogger<WorkerService> logger)
        {
            _workerRepo = adriverRepo;
            this._logger = logger;
            client = new HttpClient();
        }

        public async Task<WorkerResponse> Add(WorkerRequest workerRequest)
        {
            var responseModel = await _workerRepo.AddWorker(workerRequest);

            return responseModel;
        }

        public Paging<WorkerResponse> GetAll(GridifyQuery gridifyQuery)
        {
            var worker = _workerRepo.GetAll().Gridify(gridifyQuery);
            return worker.Adapt<Paging<WorkerResponse>>();
        }

        public async Task<WorkerResponse> GetById(int id)
        {
            var worker = await _workerRepo.GetWorkerById(id);
                try
                {
                    var stopResponse = await client.GetFromJsonAsync<StopResponse>($"{RouteServiceBaseUrl}api/route/stop/{worker.StopId}");
                    var workerResponse = worker.Adapt<WorkerResponse>();
                    workerResponse.StopResponse = stopResponse;
                    return workerResponse;

                }
                catch (HttpRequestException ex)
                {
                    throw new Exception($"HTTP request failed: {ex.Message}");
                }
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

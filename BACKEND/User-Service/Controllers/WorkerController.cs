using Gridify;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using User_Service.Services.Worker;

namespace User_Service.Controllers
{
    [Route("api/user/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpGet]
        public ActionResult GetAll([FromQuery] GridifyQuery gridifyQuery)
        {
            try
            {
                var res = _workerService.GetAll(gridifyQuery);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost]
        public async Task<ActionResult> addWorker(WorkerRequest worker)
        {
            try
            {
                var workers = await _workerService.Add(worker);
                return Ok(workers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]

        public async Task<ActionResult> GetByid(int id)
        {
            try
            {
                var response = await _workerService.GetById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _workerService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] WorkerRequest worker)
        {
            try
            {
                var re = await _workerService.Update(id, worker);
                return Ok(re);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
